// <copyright file="CreatePersonCommandHandler.cs" company="Hoff">
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

namespace VerticalSliceApi.Persons.CreatePerson
{
    /// <summary>
    /// Обработчик команды CreatePersonCommand.
    /// </summary>
    [Endpoint("api/v1/persons", "POST")]
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;
        private readonly IDistributedCache cache;
        private readonly IMapper mapper;

        public CreatePersonCommandHandler(IUnitOfWorkProvider unitOfWorkProvider,
            IDistributedCache cache,
            IMapper mapper)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.cache = cache;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            await this.cache.RemoveAsync(PersonCacheKeys.PersonsList, cancellationToken).ConfigureAwait(false);
            var person = this.mapper.Map<Person>(command);
            await this.CreatePersonAsync(person, cancellationToken);

            return person.PersonId;
        }

        private Task CreatePersonAsync(Person person, CancellationToken cancellationToken)
        {
            return this.unitOfWorkProvider.GetUnitOfWork(cancellationToken).CallStoredProcedureAsync(
                "public.create_person",
                new Dictionary<string, object>
                {
                    ["p_person"] = person,
                });
        }
    }
}
