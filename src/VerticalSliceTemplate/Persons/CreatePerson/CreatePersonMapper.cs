// <copyright file="CreatePersonMapper.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;

using AutoMapper;

using VerticalSliceApi.Persons.Shared.Entities;

namespace VerticalSliceApi.Persons.CreatePerson
{
    /// <summary>
    /// Настройка автомапера для команды CreatePersonCommand.
    /// </summary>
    public class CreatePersonMapper : Profile
    {
        public CreatePersonMapper()
        {
            this.CreateMap<CreatePersonCommand, Person>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.GenderAsInt, opt => opt.Ignore());
        }
    }
}
