// <copyright file="GetPersonMapper.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using AutoMapper;

using VerticalSliceApi.Persons.Shared.Entities;

namespace VerticalSliceApi.Persons.GetPerson
{
    public class GetPersonMapper : Profile
    {
        public GetPersonMapper()
        {
            this.CreateMap<Person, Dto.Person>();
        }
    }
}
