// <copyright file="CreatePersonServicesConfiguration.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using Gems.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using VerticalSliceApi.Persons.CreatePerson.BusinessRules;

namespace VerticalSliceApi.Persons.CreatePerson
{
    public class CreatePersonServicesConfiguration : IServicesConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<BusinessRulesChecker>();
            services.AddSingleton<NotAdultBusinessRule>();
        }
    }
}
