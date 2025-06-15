// <copyright file="Startup.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using Autofac;

using Gems.CompositionRoot;
using Gems.HealthChecks;
using Gems.Logging.Mvc.Middlewares;
using Gems.Swagger;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Prometheus;

namespace VerticalSliceApi
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCompositionRoot<Startup>(this.configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.ConfigureAutofacCompositionRoot<Startup>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseHttpMetrics();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerApi(this.configuration);
            app.UseEndpointLogging();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
                endpoints.MapDefaultHealthChecks();
            });
        }
    }
}
