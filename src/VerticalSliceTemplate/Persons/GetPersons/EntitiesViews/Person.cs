// <copyright file="Person.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;

using Gems.Data.Npgsql;

using NpgsqlTypes;

namespace VerticalSliceApi.Persons.GetPersons.EntitiesViews
{
    [PgType]
    public class Person
    {
        [PgName("person_id")]
        public Guid PersonId { get; set; }

        [PgName("full_name")]
        public string FullName { get; set; }
    }
}
