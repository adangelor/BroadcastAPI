using System.ComponentModel.DataAnnotations;

namespace BroadcastApi.ViewModels
{
    public class CredentialsViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
