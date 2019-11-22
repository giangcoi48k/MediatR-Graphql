using HotChocolate.Types;
using TestGrapQL.Extensions;
using TestGrapQL.Models;
using TestGrapQL.Resolvers;

namespace TestGrapQL.GraphTypes
{
    public class PropertyType : ObjectType<Property>
    {
        protected override void Configure(IObjectTypeDescriptor<Property> descriptor)
        {
            descriptor
                .Field(t => t.Payments)
                .Type<ListType<PaymentType>>()
                .Argument("last", a => a.Type<IdType>())
                .AddResolver<PaymentResolver>();
        }
    }
}
