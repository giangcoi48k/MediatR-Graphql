using HotChocolate.Types;
using MediatrGrapQL.Models;

namespace MediatrGrapQL.Schema.QueryTypes
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
