namespace HackKU2024NourishNet.Services
{
    public class OrganizationsService
    {
        static OrganizationsService organizationsService;

        public OrganizationsService GetOrganizationsService
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
    }
}
