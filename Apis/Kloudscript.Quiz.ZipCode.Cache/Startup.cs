using Kloudscript.Quiz.ZipCode.Cache.Services;
using Kloudscript.Quiz.ZipCode.Cache.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Kloudscript.Quiz.ZipCode.Cache
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
            services.Configure<Applicationsettings>(Configuration.GetSection(nameof(Applicationsettings)));

            //Explicitly register the settings object by delegating to the IOptions object so that it can be accessed globally via AppServicesHelper.
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptionsMonitor<Applicationsettings>>().CurrentValue);

            //Response Caching...
            services.AddResponseCaching(x=>x.MaximumBodySize = 1024);

            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ZipCode Cache Microservice API",
                    Description = "A simple microservice api for ZipCode Cache.",
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


            services.AddScoped<IUSPSService, USPSService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZipCode Cache API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(10)
                };

                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                new string[] { "Accept-Encoding" };

                await next();

            });
        }
    }
}
