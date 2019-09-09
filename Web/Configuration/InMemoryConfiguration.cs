using System.Linq;

namespace FutterlisteNg.Web.Configuration
{
    public class InMemoryConfiguration : IConfiguration
    {
        public InMemoryConfiguration(string apiBaseUrl)
        {
            ApiBaseUrl = NormalizeBaseUrl(apiBaseUrl);
        }

        private string NormalizeBaseUrl(string apiBaseUrl)
        {
            if (apiBaseUrl.Last() == '/')
                return apiBaseUrl;
            return apiBaseUrl + "/";
        }

        public string ApiBaseUrl { get; set; }
    }
}