// <copyright file="DeletePersonCommandHandler.cs" company="Hoff">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Gems.Data.UnitOfWork;
using Gems.Mvc.GenericControllers;

using MediatR;

using Microsoft.Extensions.Caching.Distributed;

using VerticalSliceApi.Persons.Shared;

namespace VerticalSliceApi.Persons.DeletePerson
{
    /// <summary>
    /// Обработчик команды DeletePersonCommand.
    /// </summary>
    [Endpoint("api/v1/persons/{PersonId}", "DELETE")]
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;
        private readonly IDistributedCache cache;

        public DeletePersonCommandHandler(IUnitOfWorkProvider unitOfWorkProvider, IDistributedCache cache)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.cache = cache;
        }

        public async Task Handle(DeletePersonCommand command, CancellationToken cancellationToken)
        {
            await this.cache.RemoveAsync(PersonCacheKeys.PersonsList, cancellationToken).ConfigureAwait(false);
            await this.DeletePersonAsync(command.PersonId, cancellationToken).ConfigureAwait(false);
        }

        private Task DeletePersonAsync(Guid personId, CancellationToken cancellationToken)
        {
            return this.unitOfWorkProvider.GetUnitOfWork(cancellationToken).CallStoredProcedureAsync(
                "public.delete_person",
                new Dictionary<string, object>
                {
                    ["p_person_id"] = personId,
                });
        }
    }
}
