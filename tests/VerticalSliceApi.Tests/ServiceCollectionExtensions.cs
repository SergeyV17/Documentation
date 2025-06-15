// <copyright file="ServiceCollectionExtensions.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System.Data;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace VerticalSliceApi.Tests
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Remove<T>(this IServiceCollection services)
        {
            if (services.IsReadOnly)
            {
                throw new ReadOnlyException($"{nameof(services)} is read only");
            }

            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
            if (serviceDescriptor != null)
            {
                services.Remove(serviceDescriptor);
            }

            return services;
        }
    }
}
