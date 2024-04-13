using HackKU2024NourishNet.Models;
using HackKU2024NourishNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackKU2024NourishNet.Controllers
{
    [Route("/Api")]
    public class TypesController : Controller
    {
        [Route("GetType")]
        [HttpGet]
        public List<Models.Type> GetTypes(string collection)
        {
            return TypesService.GetTypesService.GetTypes();
        }

        [Route("AddType")]
        [HttpPost] public async Task<bool> AddCategory(string collection, string name)
        {
            return await TypesService.GetTypesService.AddType(collection, name);
        }
    }
}
