// <copyright file="CreatePersonCommandValidator.cs" company="Hoff">
// Copyright (c) Company. All rights reserved.
// </copyright>

using FluentValidation;

namespace VerticalSliceApi.Persons.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            this.RuleFor(m => m.FirstName)
                .NotEmpty()
                .MaximumLength(20);

            this.RuleFor(m => m.LastName)
                .NotEmpty()
                .MaximumLength(20);

            this.RuleFor(m => m.Age)
                .GreaterThanOrEqualTo(0);
        }
    }
}
