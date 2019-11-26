using HotChocolate.Types;
using System.Collections.Generic;
using MediatrGrapQL.Extensions;
using MediatrGrapQL.Models;
using MediatrGrapQL.Resolvers;

namespace MediatrGrapQL.Schema.QueryTypes
{
    public class PropertyType : ObjectType<Property>
    {
        protected override void Configure(IObjectTypeDescriptor<Property> descriptor)
        {
            descriptor.Name(nameof(PropertyType));
            descriptor
                .Field(t => t.Payments)
                .Type<ListType<PaymentType>>()
                .AddResolver<PaymentResolver, IEnumerable<Payment>>();
        }
    }
}
