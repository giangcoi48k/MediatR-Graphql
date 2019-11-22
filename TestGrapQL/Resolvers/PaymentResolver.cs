using HotChocolate.Resolvers;
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

        public void AddArguments(IObjectFieldDescriptor descriptor)
        {
            descriptor.Argument("last", a => a.Type<IdType>());
        }

        public void ResolveArguments(IResolverContext context)
        {
            Id = context.Parent<Property>().Id;
            Last = context.Argument<int?>("last");
        }

        private class Handler : IBaseResolverHandler<PaymentResolver>
        {
            private readonly PaymentService _paymentService;

            public Handler(PaymentService paymentService)
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
