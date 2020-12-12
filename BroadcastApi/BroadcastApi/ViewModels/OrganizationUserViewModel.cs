using BroadcastApi.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace BroadcastApi.ViewModels
{
    public class OrganizationUserViewModel : OrganizationUser
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}
