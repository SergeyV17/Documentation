// <copyright file="UpdatePersonCommandValidator.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using FluentValidation;

using VerticalSliceApi.Persons.CreatePerson;

namespace VerticalSliceApi.Persons.UpdatePerson
{
    public class UpdatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public UpdatePersonCommandValidator()
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
