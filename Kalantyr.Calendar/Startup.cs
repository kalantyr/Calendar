using System;
using System.Net.Http;
using Kalantyr.Auth.Client;
using Kalantyr.Auth.Models.Config;
using Kalantyr.Auth.Services;
using Kalantyr.Auth.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Kalantyr.Calendar
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CalendarServiceConfig>(_configuration.GetSection("CalendarService"));

            services.AddSingleton<IAuthClient>(sp => new AuthClient(
                sp.GetService<IHttpClientFactory>(),
                sp.GetService<IOptions<CalendarServiceConfig>>().Value.ApiKey));

            services.AddSingleton<ICalendarService>(sp => new CalendarService(
                sp.GetService<IAuthClient>(),
                sp.GetService<IOptions<CalendarServiceConfig>>()));

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseEndpoints(routeBuilder => routeBuilder.MapControllers());
        }
    }
}
