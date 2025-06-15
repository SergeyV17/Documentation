// <copyright file="GetPersonQueryHandler.cs" company="Company">
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

using VerticalSliceApi.Persons.Shared.Entities;

namespace VerticalSliceApi.Persons.GetPerson
{
    /// <summary>
    /// Обработчик запроса GetPersonQuery.
    /// </summary>
    [Endpoint("api/v1/persons/{PersonId}", "GET")]
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, Dto.Person>
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;
        private readonly IMapper mapper;

        public GetPersonQueryHandler(IUnitOfWorkProvider unitOfWorkProvider, IMapper mapper)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.mapper = mapper;
        }

        public async Task<Dto.Person> Handle(GetPersonQuery query, CancellationToken cancellationToken)
        {
            var person = await this.GetPersonAsync(query.PersonId, cancellationToken).ConfigureAwait(false);

            return this.mapper.Map<Dto.Person>(person);
        }

        private Task<Person> GetPersonAsync(Guid personId, CancellationToken cancellationToken)
        {
            return this.unitOfWorkProvider.GetUnitOfWork(cancellationToken).CallTableFunctionFirstAsync<Person>(
                "public.get_person_by_id",
                new Dictionary<string, object>
                {
                    ["p_person_id"] = personId,
                });
        }
    }
}
