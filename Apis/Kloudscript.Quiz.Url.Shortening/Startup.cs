using Kloudscript.Quiz.Url.Shortening.Context;
using Kloudscript.Quiz.Url.Shortening.Services;
using Kloudscript.Quiz.Url.Shortening.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Kloudscript.Quiz.Url.Shortening
{
    [ExcludeFromCodeCoverage]
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
            services.AddControllers();
            // Register the IOptions object
            services.Configure<ApplicationSettings>(Configuration.GetSection(nameof(ApplicationSettings)));

            //Explicitly register the settings object by delegating to the IOptions object so that it can be accessed globally via AppServicesHelper.
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptionsMonitor<ApplicationSettings>>().CurrentValue);

            services.AddDbContext<UrlShortenerContext>(options =>
                options.UseSqlServer(
                    Configuration.GetSection("ApplicationSettings:ConnectionStrings:DefaultConnection").Value,
                    b => b.MigrationsAssembly(typeof(UrlShortenerContext).Assembly.FullName)));

            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Url.Shortening Microservice API",
                    Description = "A simple microservice api for Url.Shortening.",
                    TermsOfService = new Uri("https://kloudscript.com/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Kloudscript",
                        Email = "admin@Kloudscript.com",
                        Url = new Uri("https://kloudscript.com/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://kloudscript.com/"),
                    }
                }); 
            });

            services.AddScoped<IShortUrlService, ShortUrlService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Url.Shortening V1");
            });

            app.UseSwagger();
             
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
