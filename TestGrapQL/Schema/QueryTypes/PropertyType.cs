using HotChocolate.Types;
using TestGrapQL.Extensions;
using TestGrapQL.Models;
using TestGrapQL.Resolvers;

namespace TestGrapQL.Schema.QueryTypes
{
    public class PropertyType : ObjectType<Property>
    {
        protected override void Configure(IObjectTypeDescriptor<Property> descriptor)
        {
            descriptor.Name(nameof(PropertyType));
            descriptor
                .Field(t => t.Payments)
                .Type<ListType<PaymentType>>()
                .AddResolver<PaymentResolver>();
        }
    }
}
