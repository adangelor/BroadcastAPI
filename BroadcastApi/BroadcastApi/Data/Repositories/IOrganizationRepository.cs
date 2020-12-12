using BroadcastApi.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BroadcastApi.Data.Repositories
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        Task<List<Organization>> GetMyOrganizations(string userName);
    }
}
