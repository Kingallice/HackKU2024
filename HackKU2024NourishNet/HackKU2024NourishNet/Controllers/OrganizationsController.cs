using HackKU2024NourishNet.Models;
using HackKU2024NourishNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackKU2024NourishNet.Controllers
{
    [Route("/API")]
    public class OrganizationsController : Controller
    {
        [Route("GetOrganizations")]
        [HttpGet]
        public List<Organization> GetOrganizations()
        {
            return OrganizationsService.GetOrganizationsService.GetOrganizations();
        }

        [Route("AddOrganization")]
        [HttpPost]
        public Task<bool> AddOrganization(string name, string orgType, string firstName, string lastName, string email, string phone, string password)
        {
            return OrganizationsService.GetOrganizationsService.AddOrganization(name, orgType, firstName, lastName, email, phone, password);
        }
    }
}
