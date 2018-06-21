using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserProfileManager.Data.DbContexts;
using UserProfileManager.Data.Repositories;
using UserProfileManager.Data.Repositories.Base;
using Newtonsoft.Json;

namespace UserProfileManager
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration,
            IHostingEnvironment env
        )
        {
            this.Configuration = configuration;
            this.Env = env;

            // This step can be skipped - injected IConfiguration already contain built configuration
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            //builder.AddEnvironmentVariables();
            //var builtConfiguration = builder.Build();
            //this.Configuration = builtConfiguration;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DB configuration
            string connectionString = Configuration.GetConnectionString("DataAccessMicrosoftSqlProvider");
            string dbContextAssemblyName = typeof(ApplicationDbContext).Assembly.GetName().Name;
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(dbContextAssemblyName))
            );

            // Register servies
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();

            // Cors
            services.AddCors();

            // Mvc
            services.AddMvc(options => {
                // Regiter custom filters
                //options.Filters.Add(typeof());
            })
            .AddJsonOptions(options => {
                // Config serialization
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        { 
            this.InitializeDatabase(app, env);

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Cors
            if (env.IsDevelopment())
            {
                app.UseCors(options =>
                {
                    options.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            }
            else
            {
                // TODO: add if needed
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app, IHostingEnvironment env)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var appDbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                // Ensure DB is created (do not apply migrations if exists)
                // appDbContext.Database.EnsureCreated();
            }
        }
    }
}
