// <copyright file="CreatePersonTests.Validation.cs" company="ТОО "К-АПП"">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentValidation;

using Microsoft.AspNetCore.Http;

using NUnit.Framework;

using VerticalSliceApi.Persons.CreatePerson;
using VerticalSliceApi.Persons.CreatePerson.Validation;

namespace VerticalSliceApi.Tests.Persons.CreatePerson
{
    public partial class CreatePersonTests
    {
        private class Validation
        {
            /// <summary>
            /// Если имя или фамилия не заполнены
            /// то,
            /// - срабатывает валидация, возвращается 400.
            /// </summary>
            [Test]
            public async Task Handle_EmptyFields_Return400Response()
            {
                // Arrange
                await using var scope = new TestScope(new CreatePersonCommand());

                scope.BuildServiceProvider();
                var invoker = scope.GetRequiredService<MediatorInvoker>();

                // Act
                var (_, error) = await invoker
                    .TrySend(scope.CreatePersonCommand, CancellationToken.None)
                    .ConfigureAwait(false);

                // Assert
                error.Exception.Should().BeOfType<ValidationException>();
                error.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
                error.Error.Should().NotBeNull();
                error.Error.IsBusiness.Should().BeFalse();
                error.Error.Message.Should().NotBeEmpty();
                error.Error.Errors.Should().Contain(
                    new List<string>
                    {
                        CreatePersonCommandValidator.FirstNameNotEmptyMessage,
                        CreatePersonCommandValidator.LastNameNotEmptyMessage
                    });
            }
        }
    }
}
