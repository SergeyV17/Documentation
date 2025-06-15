// <copyright file="MediatorInvoker.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

using Gems.Mvc;
using Gems.Mvc.Filters.Errors;

using MediatR;

namespace VerticalSliceApi.Tests
{
    public class MediatorInvoker
    {
        private readonly IMediator mediator;
        private readonly IConverter<Exception, BusinessErrorViewModel> exceptionConverter;

        public MediatorInvoker(IMediator mediator, IConverter<Exception, BusinessErrorViewModel> exceptionConverter)
        {
            this.mediator = mediator;
            this.exceptionConverter = exceptionConverter;
        }

        public async Task<(TResponse, BusinessErrorViewModel)> TrySend<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await this.mediator.Send(request, cancellationToken);
                return (result, null);
            }
            catch (Exception e)
            {
                return (default, this.exceptionConverter.Convert(e));
            }
        }

        public async Task<BusinessErrorViewModel> TrySend(IRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await this.mediator.Send(request, cancellationToken);
                return default;
            }
            catch (Exception e)
            {
                return this.exceptionConverter.Convert(e);
            }
        }
    }
}
