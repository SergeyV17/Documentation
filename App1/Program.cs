// <copyright file="Program.cs" company="Hoff">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Web;

namespace VerticalSliceApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Debug("Init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped because of exception");

                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureLogging(logging => logging.ClearProviders())
                .UseNLog();
        }
    }
}
