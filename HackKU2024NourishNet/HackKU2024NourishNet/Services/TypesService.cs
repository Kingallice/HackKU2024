using System.ComponentModel;
using HackKU2024NourishNet.Models;
using HackKU2024NourishNet.Configuration;
using MongoDB.Driver;

namespace HackKU2024NourishNet.Services
{
    public class TypesService
    {
        static TypesService typesService;

        public static TypesService GetTypesService
        {
            get
            {
                if (typesService == null)
                {
                    typesService = new TypesService();
                }
                return typesService;
            }
        }

        public List<Models.Type> GetTypes(string collection)
        {
            try
            {
                FilterDefinition<Models.Type> filter = Builders<Models.Type>.Filter.Empty;
                SortDefinition<Models.Type> sort = Builders<Models.Type>.Sort.Ascending("name");
                FindOptions<Models.Type> options = new FindOptions<Models.Type>()
                {
                    Sort = sort
                };
                return MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Models.Type>(collection).FindAsync(filter, options).Result.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<Models.Type>();
        }

        public async Task<bool> AddType(string collection, string name)
        {
            try
            {
                Models.Type category = new Models.Type()
                {
                    ObjectID = MongoDB.Bson.ObjectId.GenerateNewId(),
                    Name = name
                };

                MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Models.Type>(collection).InsertOne(category);
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
