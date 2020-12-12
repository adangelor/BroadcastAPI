using BroadcastApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BroadcastApi.Data.Repositories
{
    public interface IOrganizationUserRepository : IGenericRepository<OrganizationUser>
    {
        Organization GetUserOrganization(string email);

        Organization GetUserOrganizationByUserId(Guid Id);
        IList<OrganizationUser> GetOrganizationUsers(int organizationid);
        Task<List<OrganizationUser>> GetAllOrganizationUsersAsync(int OrganizationId);
        Task<OrganizationUser> GetByIdAsyncWithUserData(int value);
    }
}
