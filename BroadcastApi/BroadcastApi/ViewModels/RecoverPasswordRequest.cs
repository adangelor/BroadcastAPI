using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BroadcastApi.ViewModels
{
    public class RecoverPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
