using HotChocolate.Types;
using System.Collections.Generic;
using MediatrGrapQL.Models;
using MediatrGrapQL.Resolvers;
using HotChocolate;

namespace MediatrGrapQL.Schema.QueryTypes
{
    public class PropertyType : ObjectType<Property>
    {
        protected override void Configure(IObjectTypeDescriptor<Property> descriptor)
        {
            descriptor.Name(nameof(PropertyType));
            descriptor
                .Field(t => t.Payments)
                .AddResolver<PaymentResolver, IEnumerable<Payment>>();

        }
    }
}
