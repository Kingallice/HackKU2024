using System.ComponentModel;
using HackKU2024NourishNet.Models;
using HackKU2024NourishNet.Configuration;
using MongoDB.Driver;

namespace HackKU2024NourishNet.Services
{
    public class CategoriesService
    {
        static CategoriesService categoriesService;

        public static CategoriesService  GetCategoriesService
        {
            get
            {
                if (categoriesService == null)
                {
                    categoriesService = new CategoriesService();
                }
                return categoriesService;
            }
        }

        public List<Category> GetCategories()
        {
            MongoClient client = MongoDBConfig.GetMongoDBConfig.MongoDBClient;

            return client.GetDatabase("NetNourish").GetCollection<Category>("categories").AsQueryable().ToList();
        }
    }
}
