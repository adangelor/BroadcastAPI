using System.ComponentModel.DataAnnotations;

namespace BroadcastApi.ViewModels
{
    public class ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
