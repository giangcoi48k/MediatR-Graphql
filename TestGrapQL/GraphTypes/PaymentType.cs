using HotChocolate.Types;
using TestGrapQL.Models;

namespace TestGrapQL.GraphTypes
{
    public class PaymentType : ObjectType<Payment>
    {
        protected override void Configure(IObjectTypeDescriptor<Payment> descriptor)
        {
            descriptor.Field(t => t.PropertyId).Ignore();
        }
    }
}
