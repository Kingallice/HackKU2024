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
                return "mongodb+srv://NetNourishAPI:<password>@hackku2024.vaslzso.mongodb.net/?retryWrites=true&w=majority&appName=HackKU2024".Replace("<password>", Environment.GetEnvironmentVariable("MongoDBPassword"));
            }
        }

        public MongoClient MongoDBClient
        {
            get
            {
                MongoClientSettings settings = MongoClientSettings.FromConnectionString(ConnectionString);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);

                return new MongoClient(settings);
            }
        }
    }
}
