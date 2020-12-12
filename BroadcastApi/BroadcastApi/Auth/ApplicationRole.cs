using Microsoft.AspNetCore.Identity;
using System;

namespace BroadcastApi.Auth
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
