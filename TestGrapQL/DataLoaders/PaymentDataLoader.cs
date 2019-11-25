using GreenDonut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestGrapQL.Models;
using TestGrapQL.Services;

namespace TestGrapQL.DataLoaders
{
    public class PaymentDataLoader : DataLoaderBase<int, Payment>
    {
        private readonly PaymentService _paymentService;

        public PaymentDataLoader(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        protected override async Task<IReadOnlyList<Result<Payment>>> FetchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            return await _paymentService.GetAllForPropertiesAsync(keys, null);
        }
    }
}
