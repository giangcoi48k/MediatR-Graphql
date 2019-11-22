using HotChocolate.Resolvers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestGrapQL.Models;
using TestGrapQL.Services;

namespace TestGrapQL.Resolvers
{
    public sealed class PaymentResolver : BaseRequest
    {
        public int Id { get; set; }

        public int? Last { get; set; }

        public PaymentResolver(IResolverContext context) : base(context)
        {
            Last = context.Argument<int?>("last");
            Id = context.Parent<Property>().Id;
        }

        private class PaymentResolverHandle : IRequestHandler<PaymentResolver, object>
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
