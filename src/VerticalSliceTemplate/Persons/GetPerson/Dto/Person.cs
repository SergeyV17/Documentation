// <copyright file="Person.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;

using VerticalSliceApi.Persons.Shared.Entities;

namespace VerticalSliceApi.Persons.GetPerson.Dto
{
    public class Person
    {
        public Guid PersonId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}
