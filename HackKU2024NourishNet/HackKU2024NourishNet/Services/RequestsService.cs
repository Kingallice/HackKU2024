namespace HackKU2024NourishNet.Services
{
    public class RequestsService
    {
        static RequestsService requestsService;

        public static RequestsService GetRequestsService
        {
            get
            {
                if (requestsService == null)
                {
                    requestsService = new RequestsService();
                }
                return requestsService;
            }
        }
    }
}
