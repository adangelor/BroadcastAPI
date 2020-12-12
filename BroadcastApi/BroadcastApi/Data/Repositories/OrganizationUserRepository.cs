using BroadcastApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BroadcastApi.Data.Repositories
{
    public class OrganizationUserRepository : GenericRepository<OrganizationUser>, IOrganizationUserRepository
    {
        readonly ApplicationDbContext context;
        public OrganizationUserRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public Organization GetUserOrganization(string email)
        {
            return context.OrganizationUsers
                .Include(ou => ou.Organization)
                .Where(r => r.User.Email == email)
                .FirstOrDefault().Organization;
        }

        public IList<OrganizationUser> GetOrganizationUsers(int organizationid)
        {
            return context.OrganizationUsers
                .Where(r => r.OrganizationId == organizationid)
                .ToList();
        }

        public Organization GetUserOrganizationByUserId(Guid Id)
        {
            return context.OrganizationUsers
                .Include(ou => ou.Organization)
                .Where(r => r.ApplicationUserId == Id)
                .FirstOrDefault().Organization;
        }

        public async Task<List<OrganizationUser>> GetAllOrganizationUsersAsync(int OrganizationId)
        {
            return await context.OrganizationUsers.Include(o => o.User)
                .Where(ou => ou.OrganizationId == OrganizationId)
                .ToListAsync();
        }

        public async Task<OrganizationUser> GetByIdAsyncWithUserData(int Id)
        {
            return await context.OrganizationUsers
                .Include(ou => ou.User)
                .FirstOrDefaultAsync(ou => ou.Id == Id);
        }
    }
}
