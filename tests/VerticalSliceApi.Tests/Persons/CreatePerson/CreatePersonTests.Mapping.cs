// <copyright file="CreatePersonTests.Validation.cs" company="ТОО "К-АПП"">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System.Threading.Tasks;

using AutoMapper;

using NUnit.Framework;

namespace VerticalSliceApi.Tests.Persons.CreatePerson
{
    public partial class CreatePersonTests
    {
        private class Mapping
        {
            /// <summary>
            /// Проверяет на валидность маппинга сущностей.
            /// </summary>
            [Test]
            public async Task AssertConfigurationIsValid_CheckMapperValidation_DoesNotThrowException()
            {
                // Arrange
                await using var scope = new TestScope();
                scope.BuildServiceProvider();

                var mapper = scope.GetRequiredService<IMapper>();

                // Assert
                Assert.DoesNotThrow(ValidateMapping);
                return;

                // Act
                void ValidateMapping() => mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }
        }
    }
}
