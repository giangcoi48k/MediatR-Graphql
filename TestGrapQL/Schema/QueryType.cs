using HotChocolate.Types;
using TestGrapQL.Extensions;
using TestGrapQL.GraphTypes;
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
                .Argument("last", a => a.Type<IntType>())
                .AddResolver<PropertyResolver>();
        }
    }
}
