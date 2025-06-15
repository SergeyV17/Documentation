// <copyright file="GetPersonsQueryHandler.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Gems.Data.UnitOfWork;
using Gems.Mvc.GenericControllers;

using MediatR;

using VerticalSliceApi.Persons.GetPersons.EntitiesViews;

namespace VerticalSliceApi.Persons.GetPersons
{
    /// <summary>
    /// Обработчик запроса GetPersonsQuery.
    /// </summary>
    [Endpoint("api/v1/persons", "GET")]
    public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, List<Person>>
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;

        public GetPersonsQueryHandler(IUnitOfWorkProvider unitOfWorkProvider)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
        }

        public Task<List<Person>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            return this.unitOfWorkProvider.GetUnitOfWork(cancellationToken).CallTableFunctionAsync<Person>(
                "public.get_persons");
        }
    }
}
