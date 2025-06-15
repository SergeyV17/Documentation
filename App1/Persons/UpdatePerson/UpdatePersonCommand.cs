// <copyright file="UpdatePersonCommand.cs" company="Hoff">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;
using System.Text.Json.Serialization;

using Gems.Data.Behaviors;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using VerticalSliceApi.Persons.Shared.Entities;

namespace VerticalSliceApi.Persons.UpdatePerson
{
    /// <summary>
    /// Данная команда обновляет пользователя в бд.
    /// </summary>
    public class UpdatePersonCommand : IRequest, IRequestUnitOfWork
    {
        /// <summary>
        /// Поле заполнится из URL: api/v1/persons/{PersonId}.
        /// </summary>
        [FromRoute]
        [JsonIgnore]
        public Guid PersonId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        /// <summary>
        /// Поле заполнится из http заголовка Authorization.
        /// </summary>
        [FromHeader]
        [JsonIgnore]
        public string Authorization { get; set; }

        /// <summary>
        /// Поле заполнится из строки запроса (query string): api/v1/persons/{PersonId}?RedirectUrl=your_redirect_url.
        /// </summary>
        [FromQuery]
        [JsonIgnore]
        public string RedirectUrl { get; set; }
    }
}
