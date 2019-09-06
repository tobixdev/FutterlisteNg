namespace FutterlisteNg.Web.Configuration
{
    public class InMemoryConfiguration : IConfiguration
    {
        public InMemoryConfiguration(string apiBaseUrl)
        {
            ApiBaseUrl = apiBaseUrl;
        }

        public string ApiBaseUrl { get; }
    }
}