using HackKU2024NourishNet.Models;
using HackKU2024NourishNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackKU2024NourishNet.Controllers
{
    [Route("/Api")]
    public class CategoryController : Controller
    {
        [Route("GetCategories")]
        [HttpGet]
        public List<Category> GetCategories()
        {
            return CategoriesService.GetCategoriesService.GetCategories();
        }

        [Route("AddCategory")]
        [HttpPost] public async Task<bool> AddCategory(string categoryName)
        {
            return await CategoriesService.GetCategoriesService.AddCategory(categoryName);
        }
    }
}
