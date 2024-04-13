namespace HackKU2024NourishNet.Services
{
    public class UsersService
    {
        static UsersService usersService;

        public UsersService GetUsersService
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
    }
}
