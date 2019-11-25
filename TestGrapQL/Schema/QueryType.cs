using HotChocolate.Types;
using TestGrapQL.Attributes;
using TestGrapQL.Extensions;
using TestGrapQL.Resolvers;
using TestGrapQL.Schema.QueryTypes;

namespace TestGrapQL.Schema
{
    [GraphQueryType]
    public class QueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor
                .Field("properties")
                .Type<ListType<NonNullType<PropertyType>>>()
                .AddResolver<PropertiesResolver>();

            descriptor
                .Field("property")
                .Type<PropertyType>()
                .AddResolver<PropertyResolver>();
        }
    }
}
