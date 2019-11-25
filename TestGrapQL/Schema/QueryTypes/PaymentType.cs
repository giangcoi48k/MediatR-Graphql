using HotChocolate.Types;
using TestGrapQL.Models;

namespace TestGrapQL.Schema.QueryTypes
{
    public class PaymentType : ObjectType<Payment>
    {
        protected override void Configure(IObjectTypeDescriptor<Payment> descriptor)
        {
            descriptor.Name(nameof(PaymentType));

            descriptor.Field(t => t.PropertyId).Ignore();
        }
    }
}
