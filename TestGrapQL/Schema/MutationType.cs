using HotChocolate.Types;
using TestGrapQL.Attributes;
using TestGrapQL.Extensions;
using TestGrapQL.Models;
using TestGrapQL.Resolvers;
using TestGrapQL.Schema.InputTypes;

namespace TestGrapQL.Schema
{
    [GraphMutationType]
    public class MutationType: ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("addProperty")
                .Type<NonNullType<PropertyInputType>>()
                .AddResolver<AddPropertyResolver, Property>();
        }
    }
}
