// <copyright file="CreatePersonCommand.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;

using Gems.Data.Behaviors;

using MediatR;

using VerticalSliceApi.Persons.Shared.Entities;

namespace VerticalSliceApi.Persons.CreatePerson
{
    /// <summary>
    /// Данная команда создает пользователя в бд.
    /// </summary>
    public class CreatePersonCommand : IRequest<Guid>, IRequestUnitOfWork
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}
