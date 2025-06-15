// <copyright file="GetPersonsQuery.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System.Collections.Generic;

using Gems.Caching.Behaviors;

using MediatR;

using VerticalSliceApi.Persons.GetPersons.EntitiesViews;
using VerticalSliceApi.Persons.Shared;

namespace VerticalSliceApi.Persons.GetPersons
{
    /// <summary>
    /// Данный запрос выбирает список пользователей из бд.
    /// </summary>
    public class GetPersonsQuery : IRequest<List<Person>>, IRequestCache
    {
        public string GetCacheKey()
        {
            return PersonCacheKeys.PersonsList;
        }
    }
}
