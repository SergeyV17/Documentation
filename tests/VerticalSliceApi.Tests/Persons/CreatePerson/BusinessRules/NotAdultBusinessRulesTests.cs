// <copyright file="NotAdultBusinessRulesTests.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using FluentAssertions;

using NUnit.Framework;

using VerticalSliceApi.Persons.CreatePerson;
using VerticalSliceApi.Persons.CreatePerson.BusinessRules;

namespace VerticalSliceApi.Tests.Persons.CreatePerson.BusinessRules
{
    [Parallelizable]
    public class NotAdultBusinessRulesTests
    {
        [Test]
        public void IsBroken_PersonNotAdult_ReturnsErrorMessage()
        {
            // Arrange
            var createPersonCommand = new CreatePersonCommand()
            {
                FirstName = "Sergey",
                LastName = "Sergeevich",
                Age = 17
            };

            var notAdultBusinessRule = new NotAdultBusinessRule();

            // Act
            var isBroken = notAdultBusinessRule.IsBroken(createPersonCommand, out var errorMessage);

            // Assert
            isBroken.Should().BeTrue();
            errorMessage.Should().NotBeNull().And.Be(NotAdultBusinessRule.ErrorMessage);
        }

        [Test]
        public void IsBroken_PersonAdult_DoesntReturnErrorMessage()
        {
            // Arrange
            var createPersonCommand = new CreatePersonCommand()
            {
                FirstName = "Sergey",
                LastName = "Sergeevich",
                Age = 18
            };

            var notAdultBusinessRule = new NotAdultBusinessRule();

            // Act
            var isBroken = notAdultBusinessRule.IsBroken(createPersonCommand, out var errorMessage);

            // Assert
            isBroken.Should().BeFalse();
            errorMessage.Should().BeEmpty();
        }
    }
}
