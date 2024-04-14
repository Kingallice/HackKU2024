using HackKU2024NourishNet.Configuration;
using HackKU2024NourishNet.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace HackKU2024NourishNet.Services
{
    public class TicketsService
    {
        static TicketsService ticketsService;

        public static TicketsService GetTicketsService
        {
            get
            {
                if (ticketsService == null)
                {
                    ticketsService = new TicketsService();
                }
                return ticketsService;
            }
        }

        public List<Ticket> GetTickets(int page)
        {
            try
            {
                FilterDefinition<Ticket> filter = Builders<Ticket>.Filter.Empty;
                SortDefinition<Ticket> sort = Builders<Ticket>.Sort.Ascending("created_at");
                FindOptions<Ticket> options = new FindOptions<Ticket>()
                {
                    Sort = sort,
                    Skip = page*50
                };
                return MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Ticket>("tickets").FindAsync(filter, options).Result.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<Ticket>();
        }

        public async Task<bool> CreateTicket(string userId, string categoryId, string ticketType, string itemName, double itemQuantity, string itemLabel)
        {
            try
            {
                User user = UsersService.GetUsersService.GetUserById(userId);
                Ticket ticket = new Ticket()
                {
                    ObjectId = ObjectId.GenerateNewId(),
                    OrgId = user.Org,
                    CategoryId = ObjectId.Parse(categoryId),
                    TicketTypeId = ObjectId.Parse(ticketType),
                    ItemName = itemName,
                    ItemQuantity = itemQuantity,
                    ItemLabel = itemLabel,
                    IsClaimed = false,
                    CreatedAt = DateTime.Now
                };
                ticket.OrgTypeId = OrganizationsService.GetOrganizationsService.GetOrganizationById(user.OrgId).OrgType;

                MongoDBConfig.GetMongoDBConfig.DEFAULT.GetCollection<Ticket>("tickets").InsertOne(ticket);
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
