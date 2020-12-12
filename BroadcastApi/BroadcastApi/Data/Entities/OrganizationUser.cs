using BroadcastApi.Auth;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroadcastApi.Data.Entities
{
    public class OrganizationUser : IEntity
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }
        public Guid ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser User { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}
