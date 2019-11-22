using HotChocolate.Types;
using TestGrapQL.Extensions;
using TestGrapQL.Resolvers;

namespace TestGrapQL.Schema
{
    public class QueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor
                .Field("properties")
                .Type<ListType<NonNullType<PropertyType>>>()
                .AddResolver<PropertyResolver>();
        }
    }
}
