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
            FilterDefinition<Category> filter = Builders<Category>.Filter.Empty;
            SortDefinition<Category> sort = Builders<Category>.Sort.Ascending("name");
            FindOptions<Category> options = new FindOptions<Category>()
            {
                Sort = sort
            };

            return MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Category>("categories").FindAsync(filter, options).Result.ToList();
        }

        public async Task<bool> AddCategory(string categoryName)
        {
            try
            {
                Category category = new Category()
                {
                    ObjectID = MongoDB.Bson.ObjectId.GenerateNewId(),
                    Name = categoryName
                };

                MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Category>("categories").InsertOne(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
