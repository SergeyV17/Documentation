// <copyright file="Person.cs" company="Hoff">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;

using Gems.Data.Npgsql;

using NpgsqlTypes;

namespace VerticalSliceApi.Persons.Shared.Entities
{
    [PgType("public.person_type")]
    public class Person
    {
        [PgName("person_id")]
        public Guid PersonId { get; set; }

        [PgName("first_name")]
        public string FirstName { get; set; }

        [PgName("last_name")]
        public string LastName { get; set; }

        [PgName("age")]
        public int Age { get; set; }

        [PgName("__ignore__")]
        public Gender Gender { get; set; }

        [PgName("gender")]
        public int GenderAsInt
        {
            get => (int)this.Gender;
            set => this.Gender = (Gender)Enum.ToObject(typeof(Gender), value);
        }
    }
}
