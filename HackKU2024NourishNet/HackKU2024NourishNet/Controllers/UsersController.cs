using HackKU2024NourishNet.Models;
using HackKU2024NourishNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackKU2024NourishNet.Controllers
{
    [Route("/API")]
    public class UsersController : Controller
    {
        [Route("GetUsers")]
        [HttpGet]
        public List<User> GetUsers()
        {
            return UsersService.GetUsersService.GetUsers();
        }

        [Route("GetUserByEmail")]
        [HttpGet]
        public User GetUserByEmail(string email)
        {
            return UsersService.GetUsersService.GetUserByEmail(email);
        }

        [Route("GetUserById")]
        [HttpGet]
        public User GetUserById(string id)
        {
            return UsersService.GetUsersService.GetUserById(id);
        }

        [Route("GetUsersByOrg")]
        [HttpGet]
        public List<User> GetUsersByOrg(string orgId)
        {
            return UsersService.GetUsersService.GetUsersByOrganization(orgId);
        }

        [Route("Login")]
        [HttpPost]
        public string LoginUser(string email, string password)
        {
            return UsersService.GetUsersService.LoginUser(email, password);
        }

        [Route("ValidateLogin")]
        [HttpPost]
        public bool ValidateLogin(string id, string cert)
        {
            return UsersService.GetUsersService.ValidateLogin(id, cert);
        }
    }
}
