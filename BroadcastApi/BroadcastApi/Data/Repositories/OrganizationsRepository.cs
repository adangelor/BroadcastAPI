using BroadcastApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BroadcastApi.Data.Repositories
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        readonly ApplicationDbContext context;
        public OrganizationRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;

        }

        public Task<List<Organization>> GetMyOrganizations(string userName)
        {
            return (from o in context.Organizations
                    join ou in context.OrganizationUsers
                    on o.Id equals ou.OrganizationId
                    where ou.User.UserName == userName
                    select o).ToListAsync();
        }

        public async Task<int?> GetOrganizationIdByHostNameAsync(string HostName)
        {
            if (HostName.Contains(":"))
            {
                HostName = HostName.Split(":")[0];
            }
            if (HostName == "https://localhost")
            {
                return 1;
            }
            var organization = await context.Organizations.FirstOrDefaultAsync(o => o.URL == HostName);
            return organization?.Id;
        }
    }
}
