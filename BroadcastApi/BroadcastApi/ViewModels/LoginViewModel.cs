using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BroadcastApi.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string PassWord { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnURL { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
