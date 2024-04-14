using HackKU2024NourishNet.Models;
using HackKU2024NourishNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackKU2024NourishNet.Controllers
{
    [Route("/API")]
    public class TicketsController : Controller
    {
        [Route("GetTickets")]
        [HttpGet]
        public List<Ticket> GetTickets(int page = 1)
        {
            return TicketsService.GetTicketsService.GetTickets(page);
        }

        [Route("CreateTicket")]
        [HttpPost]
        public Task<bool> CreateTicket(string userId, string typeId, string categoryId, string itemName, double itemQuantity, string itemLabel)
        {
            return TicketsService.GetTicketsService.CreateTicket(userId, categoryId, typeId, itemName, itemQuantity, itemLabel);
        }
    }
}
