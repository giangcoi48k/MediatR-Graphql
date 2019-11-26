using GreenDonut;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestGrapQL.Models;
using TestGrapQL.Services;

namespace TestGrapQL.Resolvers
{
    public sealed class PaymentResolver : IBaseResolver<IEnumerable<Payment>>
    {
        private IResolverContext _context;

        public int Id { get; set; }

        public int? Last { get; set; }

        public void AddArguments(IObjectFieldDescriptor descriptor)
        {
            descriptor.Argument("last", a => a.Type<IdType>());
        }

        public void ResolveArguments(IResolverContext context)
        {
            _context = context;
            Id = context.Parent<Property>().Id;
            Last = context.Argument<int?>("last");
        }

        private class Handler : IBaseResolverHandler<PaymentResolver, IEnumerable<Payment>>
        {
            private readonly PaymentService _paymentService;

            public Handler(PaymentService paymentService)
            {
                _paymentService = paymentService;
            }

            public async Task<IEnumerable<Payment>> Handle(PaymentResolver request, CancellationToken cancellationToken)
            {
                return await request._context.GroupDataLoader<int, Payment>(
                      GetType().FullName
                    , async keys => (await _paymentService.GetAllForPropertiesAsync(keys, request.Last)).ToLookup(t => t.PropertyId)
                ).LoadAsync(request.Id).ConfigureAwait(false);
            }
        }
    }
}
