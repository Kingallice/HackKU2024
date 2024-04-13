using HackKU2024NourishNet.Configuration;
using HackKU2024NourishNet.Models;
using MongoDB.Driver;

namespace HackKU2024NourishNet.Services
{
    public class OrganizationsService
    {
        static OrganizationsService organizationsService;

        public static OrganizationsService GetOrganizationsService
        {
            get
            {
                if (organizationsService == null)
                {
                    organizationsService = new OrganizationsService();
                }
                return organizationsService;
            }
        }

        public List<Organization> GetOrganizations()
        {
            try
            {
                FilterDefinition<Organization> filter = Builders<Organization>.Filter.Empty;
                SortDefinition<Organization> sort = Builders<Organization>.Sort.Ascending("name");
                FindOptions<Organization> options = new FindOptions<Organization>()
                {
                    Sort = sort
                };
                return MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Organization>("organizations").FindAsync(filter, options).Result.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<Organization>();
        }

        public async Task<bool> AddOrganization(string name, string orgType, string firstName, string lastName, string email, string phone, string password)
        {
            try
            {
                User mainUser = UsersService.GetUsersService.CreateNewUser(firstName, lastName, email, phone, password);
                Organization organization = new Organization()
                {
                    ObjectId = MongoDB.Bson.ObjectId.GenerateNewId(),
                    Name = name,
                    MainContact = mainUser.ObjectId,
                    OrgType = MongoDB.Bson.ObjectId.Parse(orgType),
                    CreatedAt = DateTime.Now
                };
                mainUser.Org = organization.ObjectId;
                UsersService.GetUsersService.AddUser(mainUser);

                organization.AuthorizedUsers = UsersService.GetUsersService.GetUsersByOrganization(organization.Id);

                MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Organization>("organizations").InsertOne(organization);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
