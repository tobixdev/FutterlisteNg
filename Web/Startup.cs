using FutterlisteNg.Web.Client;
using FutterlisteNg.Web.Configuration;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FutterlisteNg.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new InMemoryConfiguration("http://localhost:5000");
            services.AddSingleton(typeof(IConfiguration), configuration);
            services.AddSingleton(typeof(IUserClient), typeof(UserClient));
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
