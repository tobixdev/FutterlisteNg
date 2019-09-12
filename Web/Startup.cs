using FutterlisteNg.Web.Client;
using FutterlisteNg.Web.Configuration;
using FutterlisteNg.Web.Service;
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
            services.AddSingleton(typeof(IToastService), typeof(ToastService));
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
