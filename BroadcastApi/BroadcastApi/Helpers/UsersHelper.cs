using BroadcastApi.Auth;
using BroadcastApi.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastApi.Helpers
{
    public class UsersHelper : IUsersHelper
    {
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly RoleManager<ApplicationRole> roleManager;
        readonly IConfiguration configuration;
        public UsersHelper(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }



        public async Task<IdentityResult> AddUserAsync(ApplicationUser user, string passWord)
        {
            if (string.IsNullOrEmpty(passWord))
            {
                return await userManager.CreateAsync(user);
            }
            else
            {
                return await userManager.CreateAsync(user, passWord);
            }

        }

        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            return await userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = roleName,
                    Description = roleName
                });
            }
        }
        public async Task<IEnumerable<AuthenticationScheme>> GetAuthenticationSchemesAsync()
        {
            return await signInManager.GetExternalAuthenticationSchemesAsync();

        }

        public AuthenticationProperties ConfigureExternalAuthenticationProvider(string provider, string redirectURL)
        {
            return signInManager.ConfigureExternalAuthenticationProperties(provider, redirectURL);
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await userManager.DeleteAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }




        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await userManager.Users
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToListAsync();
        }

        public async Task<IList<ApplicationUser>> GetAllUsersByRoleAsync(string roleName)
        {
            return await userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await this.userManager.FindByIdAsync(userId);
        }


        public async Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName)
        {
            return await userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            var login = await signInManager.PasswordSignInAsync(
                    model.UserName,
                    model.PassWord,
                    model.RememberMe,
                    false);
            return login;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            return await userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string password)
        {
            return await userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(ApplicationUser user, string password)
        {
            return await signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        {
            return await signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent, bypassTwoFactor);
        }

        public async Task<IdentityResult> AddLoginAsync(ApplicationUser user, ExternalLoginInfo info)
        {
            return await userManager.AddLoginAsync(user, info);
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            await signInManager.SignInAsync(user, isPersistent);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            return await this.userManager.ConfirmEmailAsync(user, token);
        }
        public JwtToken CreateToken(LoginViewModel model)
        {
            var x = userManager.Users.SingleOrDefault(r => r.Email == model.UserName);
            var roles = userManager.GetRolesAsync(x).Result;
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, model.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            foreach (var r in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Tokens:Issuer"],
                configuration["Tokens:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(15),
                signingCredentials: credentials);
            return new JwtToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

    }
}
