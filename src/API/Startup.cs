using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configuration;
using APIService;
using APIService.ApiServices;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SQLDB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnection"),
                     sqlServerOptions =>
                     {
                         sqlServerOptions.MigrationsAssembly("SQLDB");
                     }                   
                    );
            });

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("SQLConnection")));
            services.AddHangfireServer();
            services.AddCors();
            services.AddControllers();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.ResolveDependencies();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHangfireDashboard();
            app.UseHangfireServer();
           

            app.UseRouting();

            app.UseAuthorization();
            

            app.UseHangfireDashboard();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
                endpoints.MapHangfireDashboard("/hangfire", new DashboardOptions()
                {

                });
            });
        }
    }
}
