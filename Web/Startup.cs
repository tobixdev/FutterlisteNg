using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Service;
using FutterlisteNg.Domain.Validation;
using FutterlisteNg.Web.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace FutterlisteNg.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton(typeof(IMongoDatabase),
                new MongoClient("mongodb://localhost:27017").GetDatabase("FutterlisteNg"));
            services.AddSingleton(typeof(IUserService), typeof(UserService));
            services.AddSingleton(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IToastService), typeof(ToastService));
            services.AddSingleton(typeof(IPaymentService), typeof(PaymentService));
            services.AddSingleton(typeof(IPaymentRepository), typeof(PaymentRepository));
            services.AddSingleton(typeof(UserCreateValidator));
            services.AddSingleton(typeof(UserUpdateValidator));
            services.AddSingleton(typeof(PaymentCreateValidator));
            services.AddSingleton(typeof(PaymentUpdateValidator));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}