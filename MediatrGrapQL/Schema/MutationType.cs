using HotChocolate.Types;
using MediatrGrapQL.Attributes;
using MediatrGrapQL.Extensions;
using MediatrGrapQL.Models;
using MediatrGrapQL.Resolvers;
using MediatrGrapQL.Schema.InputTypes;

namespace MediatrGrapQL.Schema
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
