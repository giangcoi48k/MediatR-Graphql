﻿using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;
using TestGrapQL.Models;
using TestGrapQL.Services;

namespace TestGrapQL.Resolvers
{
    public sealed class PaymentResolver : IBaseResolver
    {
        public int Id { get; set; }

        public int? Last { get; set; }

        public void CreateArguments(IObjectFieldDescriptor descriptor)
        {
            descriptor.Argument("last", a => a.Type<IdType>());
        }

        public void ResolveArguments(IResolverContext context)
        {
            Last = context.Argument<int?>("last");
            Id = context.Parent<Property>().Id;
        }

        private class PaymentResolverHandle : IBaseResolveHandler<PaymentResolver>
        {
            private readonly PaymentService _paymentService;

            public PaymentResolverHandle(PaymentService paymentService)
            {
                _paymentService = paymentService;
            }

            public async Task<object> Handle(PaymentResolver request, CancellationToken cancellationToken)
            {
                return await _paymentService.GetAllForPropertyAsync(request.Id, request.Last);
            }
        }
    }
}
