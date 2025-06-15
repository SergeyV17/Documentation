// <copyright file="UpdatePersonMapper.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using AutoMapper;

using VerticalSliceApi.Persons.Shared.Entities;

namespace VerticalSliceApi.Persons.UpdatePerson
{
    /// <summary>
    /// Настройка автомапера для команды UpdatePersonCommand.
    /// </summary>
    public class UpdatePersonMapper : Profile
    {
        public UpdatePersonMapper()
        {
            this.CreateMap<UpdatePersonCommand, Person>();
        }
    }
}
