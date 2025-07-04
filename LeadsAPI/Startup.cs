using LeadsAPI.Repositorios;
using LeadsAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace LeadsAPI
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
            services.AddAutoMapper(typeof(Startup));


            services.AddControllers()
                .AddNewtonsoftJson(options =>
                 {
                     options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                     {
                         NamingStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy()
                     };
                 });
            services.AddEndpointsApiExplorer();
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddSingleton<WorkshopService>();
            services.AddSingleton<LeadRepository>();
            services.AddAuthorization();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
