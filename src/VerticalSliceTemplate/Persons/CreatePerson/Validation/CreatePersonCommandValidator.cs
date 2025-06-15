// <copyright file="CreatePersonCommandValidator.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using FluentValidation;

namespace VerticalSliceApi.Persons.CreatePerson.Validation
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public const string FirstNameNotEmptyMessage = "Имя не может быть пустым";
        public const string LastNameNotEmptyMessage = "Фамилия не может быть пустым";

        public CreatePersonCommandValidator()
        {
            this.RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage(FirstNameNotEmptyMessage);

            this.RuleFor(p => p.LastName)
                .NotEmpty()
                .WithMessage(LastNameNotEmptyMessage);
        }
    }
}
