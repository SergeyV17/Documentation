// <copyright file="DeletePersonCommand.cs" company="Hoff">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;

using Gems.Data.Behaviors;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace VerticalSliceApi.Persons.DeletePerson
{
    /// <summary>
    /// Данная команда удаляет пользователя в бд.
    /// </summary>
    public class DeletePersonCommand : IRequest, IRequestUnitOfWork
    {
        [FromRoute]
        public Guid PersonId { get; set; }
    }
}
