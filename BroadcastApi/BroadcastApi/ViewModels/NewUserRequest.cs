using System.ComponentModel.DataAnnotations;

namespace BroadcastApi.ViewModels
{
    public class NewUserRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string City { get; set; }
        public string State { get; internal set; }
    }
}
