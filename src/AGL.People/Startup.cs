namespace AGL_People
{
    using AGL.People.Extensions;
    using AGL.People.Models.Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SpaServices.Webpack;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Net.Http;

    public class Startup
    {
        // hosting environment
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // read settings from appsettings and set
            Settings settings = GetConfigurationSettings();
            services.AddSingleton(settings);

            // register http client
            services.AddSingleton(new HttpClient());

            // load di services from appsettings.json file
            ValidateAdditionalService(services);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        /// <summary>
        /// Inject settings from appsettings.json file per environment
        /// </summary>
        /// <returns></returns>
        private Settings GetConfigurationSettings()
        {
            //Add Infrastructure
            Settings settings = Configuration.Get<Settings>("Settings");
            if (settings == null)
                throw new Exception("Settings cannot be read.");

            return settings;
        }

        /// <summary>
        /// Custom service load
        /// </summary>
        /// <param name="services"></param>
        private void ValidateAdditionalService(IServiceCollection services)
        {
            //register custom services from appsettings files
            services.ConfigureJsonServices(_env);
        }
    }
}