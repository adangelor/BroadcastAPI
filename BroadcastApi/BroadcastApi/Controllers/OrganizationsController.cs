using BroadcastApi.Data.Entities;
using BroadcastApi.Data.Repositories;
using BroadcastApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BroadcastApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        readonly IOrganizationRepository organizationRepository;

        public OrganizationsController(IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        // GET: api/Organizations
        [HttpGet]
        public ActionResult<IQueryable<Organization>> GetOrganizations()
        {
            return Ok(new Response(organizationRepository.GetAll(), string.Empty));
        }

        // GET: api/Organizations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(int id)
        {
            var organization = await organizationRepository.GetByIdAsync(id);

            if (organization == null)
            {
                return NotFound();
            }

            return Ok(new Response(organization, string.Empty));
        }

        // GET: api/Organizations/GetMyOrganizations
        [HttpGet("GetMyOrganizations")]
        public async Task<ActionResult<Organization>> GetMyOrganizations()
        {

            var organizations = await organizationRepository.GetMyOrganizations(User.Identity.Name);

            if (organizations == null)
            {
                return NotFound();
            }

            return Ok(new Response(organizations, string.Empty));
        }

        // PUT: api/Organizations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(int id, Organization organization)
        {
            if (id != organization.Id)
            {
                return BadRequest();
            }

            try
            {
                await organizationRepository.UpdateAsync(organization);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Organizations
        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
            await organizationRepository.CreateAsync(organization);

            return CreatedAtAction("GetOrganization", new { id = organization.Id }, organization);
        }

        // DELETE: api/Organizations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Organization>> DeleteOrganization(int id)
        {
            var organization = await organizationRepository.GetByIdAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            await organizationRepository.DeleteAsync(organization);
            return organization;
        }

        private bool OrganizationExists(int id)
        {
            return organizationRepository.GetByIdAsync(id) != null;
        }
    }
}
