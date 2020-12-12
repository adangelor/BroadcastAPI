using BroadcastApi.Auth;
using BroadcastApi.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BroadcastApi.Helpers
{
    public interface IUsersHelper
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> AddUserAsync(ApplicationUser user, string passWord);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);

        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);

        Task LogoutAsync();
        JwtToken CreateToken(LoginViewModel model);
        Task<SignInResult> ValidatePasswordAsync(ApplicationUser user, string password);
        Task CheckRoleAsync(string roleName);
        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName);
        Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string roleName);
        Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string password);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<IList<ApplicationUser>> GetAllUsersByRoleAsync(string roleName);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<IEnumerable<AuthenticationScheme>> GetAuthenticationSchemesAsync();
        AuthenticationProperties ConfigureExternalAuthenticationProvider(string provider, string redirectURL);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);
        Task<IdentityResult> AddLoginAsync(ApplicationUser user, ExternalLoginInfo info);
        Task SignInAsync(ApplicationUser user, bool isPersistent);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);

    }
}
