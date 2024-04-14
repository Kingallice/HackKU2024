using HackKU2024NourishNet.Configuration;
using HackKU2024NourishNet.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace HackKU2024NourishNet.Services
{
    public class UsersService
    {
        private class Login
        {
            [BsonId]
            public ObjectId Id { get; set; }

            [BsonElement("user_id")]
            public ObjectId UserId { get; set; }

            [BsonElement("cert")]
            public string Cert { get; set; }

            [BsonElement("created_at")]
            public DateTime CreatedAt { get; set; }
        }

        static UsersService usersService;

        public static UsersService GetUsersService
        {
            get
            {
                if (usersService == null)
                {
                    usersService = new UsersService();
                }
                return usersService;
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Empty;
                SortDefinition<User> sort = Builders<User>.Sort.Ascending("lname").Ascending("fname");
                FindOptions<User> options = new FindOptions<User>()
                {
                    Sort = sort
                };

                return MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<User>("users").FindAsync(filter, options).Result.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<User>();
        }
        public User GetUserByEmail(string email)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Where((x) => x.Email == email);
                SortDefinition<User> sort = Builders<User>.Sort.Ascending("lname").Ascending("fname");
                FindOptions<User> options = new FindOptions<User>()
                {
                    Sort = sort
                };

                return MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<User>("users").FindAsync(filter, options).Result.First();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
        public User GetUserById(string id)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Where((x) => x.ObjectId.ToString().Equals(id));
                FindOptions<User> options = new FindOptions<User>()
                {
                    Limit = 1
                };

                return MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<User>("users").FindAsync(filter, options).Result.First();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
        public User CreateNewUser(string firstName, string lastName, string email, string phone, string password)
        {
            User user = new User()
            {
                ObjectId = MongoDB.Bson.ObjectId.GenerateNewId(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone
            };
            user.Password = new PasswordHasher<User>().HashPassword(user, password);

            return user;
        }
        public List<User> GetUsersByOrganization(string orgId)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Where((x) => x.Org.ToString().Equals(orgId));
                SortDefinition<User> sort = Builders<User>.Sort.Ascending("lname").Ascending("fname");
                FindOptions<User> options = new FindOptions<User>()
                {
                    Sort = sort
                };

                return MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<User>("users").FindAsync(filter, options).Result.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<User>();
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<User>("users").InsertOne(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        public string LoginUser(string email, string password)
        {
            try
            {
                User user = GetUserByEmail(email);

                string cert = (user != null && new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, password).HasFlag(PasswordVerificationResult.Success)) ? Guid.NewGuid().ToString().Replace("-", "") : null;

                if (user != null)
                {
                    if (AuthLogin(user.Id, cert).Result) ;
                    return user.Id + ":" + cert;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public bool ValidateLogin(string id, string cert)
        {
            try
            {
                FilterDefinition<Login> filter = Builders<Login>.Filter.Where((x) => x.UserId.ToString().Equals(id));

                List<Login> items = MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Login>("logins").FindAsync(filter).Result.ToList();
                if (items.Count() == 1)
                {
                    Login item = items.First();
                    return item.Cert == cert && (DateTime.Now.Subtract(item.CreatedAt).Days < 1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
        public async Task<bool> AuthLogin(string id, string cert)
        {
            try
            {
                FilterDefinition<Login> filter = Builders<Login>.Filter.Where((x) => x.UserId.ToString().Equals(id));

                List<Login> items = MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Login>("logins").FindAsync(filter).Result.ToList();
                if (items.Count == 1)
                {
                    UpdateDefinition<Login> update = Builders<Login>.Update.Set("cert", cert).Set("created_at", DateTime.Now);

                    MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Login>("logins").UpdateOne<Login>((x) => x.UserId.ToString().Equals(id), update);
                }
                else
                {
                    Login item = new Login();
                    item.UserId = ObjectId.Parse(id);
                    item.Cert = cert;
                    item.CreatedAt = DateTime.Now;

                    MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Login>("logins").InsertOne(item);
                }
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
