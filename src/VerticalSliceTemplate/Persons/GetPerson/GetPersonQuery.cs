// <copyright file="GetPersonQuery.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;

using Gems.Mvc.Behaviors;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using VerticalSliceApi.Persons.GetPerson.Dto;

namespace VerticalSliceApi.Persons.GetPerson
{
    /// <summary>
    /// Данный запрос выбирает пользователя из бд.
    /// </summary>
    public class GetPersonQuery : IRequest<Person>, IRequestNotFound
    {
        [FromRoute]
        public Guid PersonId { get; set; }

        public string GetNotFoundErrorMessage()
        {
            return $"Не найден пользователь {this.PersonId}";
        }
    }
}
