using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkClicker.Interfaces;
using LinkClicker.Logic;
using LinkClicker.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LinkClicker
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddLogging();
            services.AddTransient<ILinkLogic, LinkLogic>();
            services.AddDbContext<LinkClickerContext>(options => options.UseNpgsql("User ID=nocgod;Password=nocgod;Host=postgres;Port=5432;Database=LinkClicker"));

        // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, LinkClickerContext ctx)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"))
                .AddDebug();

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            ctx.Database.EnsureCreated();
        }
    }
}
