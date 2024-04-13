namespace HackKU2024NourishNet.Services
{
    public class OffersService
    {
        static OffersService offersService;
        public static OffersService GetOffersService
        {
            get
            {
                if (offersService == null)
                {
                    offersService = new OffersService();
                }
                return offersService;
            }
        }
    }
}
