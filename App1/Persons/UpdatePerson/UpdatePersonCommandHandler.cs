// <copyright file="UpdatePersonCommandHandler.cs" company="Hoff">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Gems.Data.UnitOfWork;
using Gems.Mvc.GenericControllers;

using MediatR;

using Microsoft.Extensions.Caching.Distributed;

using VerticalSliceApi.Persons.Shared;
using VerticalSliceApi.Persons.Shared.Entities;

namespace VerticalSliceApi.Persons.UpdatePerson
{
    /// <summary>
    /// Обработчик команды UpdatePersonCommand.
    /// </summary>
    [Endpoint("api/v1/persons/{PersonId}", "PUT")]
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;
        private readonly IDistributedCache cache;
        private readonly IMapper mapper;

        public UpdatePersonCommandHandler(IUnitOfWorkProvider unitOfWorkProvider,
            IDistributedCache cache,
            IMapper mapper)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.cache = cache;
            this.mapper = mapper;
        }

        public async Task Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            await this.cache.RemoveAsync(PersonCacheKeys.PersonsList, cancellationToken).ConfigureAwait(false);
            var person = this.mapper.Map<Person>(command);
            await this.UpdatePersonAsync(person, cancellationToken);

            Console.WriteLine($"Authorization header equals {command.Authorization}");
            Console.WriteLine($"RedirectUrl parameter equals {command.RedirectUrl}");
        }

        private Task UpdatePersonAsync(Person person, CancellationToken cancellationToken)
        {
            return this.unitOfWorkProvider.GetUnitOfWork(cancellationToken).CallStoredProcedureAsync(
                "public.update_person",
                new Dictionary<string, object>
                {
                    ["p_person"] = person,
                });
        }
    }
}
