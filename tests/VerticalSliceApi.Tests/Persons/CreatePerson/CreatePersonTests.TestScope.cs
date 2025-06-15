// <copyright file="CreatePersonTests.TestScope.cs" company="ТОО "К-АПП"">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using Gems.Data.UnitOfWork;
using Gems.Metrics;
using Gems.Mvc;
using Gems.Mvc.Behaviors;
using Gems.Mvc.Filters;
using Gems.Mvc.Filters.Errors;

using MediatR;
using MediatR.Registration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using NUnit.Framework;

using VerticalSliceApi.Persons.CreatePerson;
using VerticalSliceApi.Persons.CreatePerson.Validation;

namespace VerticalSliceApi.Tests.Persons.CreatePerson
{
    [Parallelizable]
    public partial class CreatePersonTests
    {
        private class TestScope : IAsyncDisposable
        {
            private Mock<IUnitOfWork> unitOfWorkMock;
            private IServiceProvider serviceProvider;

            public TestScope(CreatePersonCommand createPersonCommand = null)
            {
                this.CreatePersonCommand = createPersonCommand ?? new CreatePersonCommand();
            }

            public CreatePersonCommand CreatePersonCommand { get; }

            public void BuildServiceProvider()
            {
                var configuration = BuildConfiguration();

                var services = new ServiceCollection();

                var serviceConfig = new MediatRServiceConfiguration();
                ServiceRegistrar.AddRequiredServices(services, serviceConfig);
                services.AddSingleton(_ => configuration);
                services.AddPipeline(typeof(ValidatorBehavior<,>));
                services.AddSingleton<MediatorInvoker>();
                services.AddSingleton<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
                services.AddSingleton<IRequestHandler<CreatePersonCommand, Guid>, CreatePersonCommandHandler>();
                services.AddSingleton(new Mock<IMetricsService>().Object);
                services.AddSingleton<IConverter<Exception, BusinessErrorViewModel>, ExceptionToBusinessErrorViewModelConverter>();

                var createPersonServicesConfiguration = new CreatePersonServicesConfiguration();
                createPersonServicesConfiguration.Configure(services, configuration);

                services.AddAutoMapper(cfg => cfg.AddProfile<CreatePersonMapper>());

                this.MockUnitOfWork(services);

                this.serviceProvider = services.BuildServiceProvider();
            }

            public TService GetRequiredService<TService>() where TService : notnull
            {
                return this.serviceProvider.GetRequiredService<TService>();
            }

            public ValueTask DisposeAsync()
            {
                return this.serviceProvider is IAsyncDisposable disposable
                    ? disposable.DisposeAsync()
                    : ValueTask.CompletedTask;
            }

            private static IConfiguration BuildConfiguration()
            {
                return new ConfigurationBuilder().Build();
            }

            private void MockUnitOfWork(IServiceCollection services)
            {
                this.unitOfWorkMock = new Mock<IUnitOfWork>();

                var unitOfWorkProviderMock = new Mock<IUnitOfWorkProvider>();

                unitOfWorkProviderMock
                    .Setup(s => s.GetUnitOfWork(It.IsAny<CancellationToken>()))
                    .Returns(this.unitOfWorkMock.Object);

                services.AddSingleton(unitOfWorkProviderMock.Object);
            }
        }
    }
}
