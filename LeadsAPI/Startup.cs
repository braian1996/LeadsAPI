﻿using LeadsAPI.Datos;
using LeadsAPI.Services;
using LeadsAPI.Settings;


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
            services.AddHttpClient();
            services.AddMemoryCache();

            services.AddSingleton<WorkshopService>();
            services.AddSingleton<RepositorioLead>();

            services.Configure<ExternalService>(Configuration.GetSection("ExternalServices"));

            services.AddEndpointsApiExplorer();
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
