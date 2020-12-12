using Microsoft.AspNetCore.Identity;
using System;

namespace BroadcastApi.Auth
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [PersonalData()]
        public string FirstName { get; set; }

        [PersonalData()]
        public string LastName { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
