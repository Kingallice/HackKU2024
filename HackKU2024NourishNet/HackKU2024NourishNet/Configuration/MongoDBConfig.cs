using MongoDB.Driver;

namespace HackKU2024NourishNet.Configuration
{
    public class MongoDBConfig
    {
        static MongoDBConfig mongoDBConfig;
        public static MongoDBConfig GetMongoDBConfig 
        { 
            get
            {
                if (mongoDBConfig == null)
                {
                    mongoDBConfig = new MongoDBConfig();
                }
                return mongoDBConfig;
            }
        }

        public string ConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable("MongoDBConnectionString").Replace("<password>", Environment.GetEnvironmentVariable("MongoDBPassword"));
            }
        }
    }
}
