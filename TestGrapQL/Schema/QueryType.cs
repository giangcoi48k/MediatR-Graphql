using HotChocolate.Types;
using System.Collections.Generic;
using TestGrapQL.Attributes;
using TestGrapQL.Extensions;
using TestGrapQL.Models;
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
                .AddResolver<PropertiesResolver, IEnumerable<Property>>();

            descriptor
                .Field("property")
                .Type<PropertyType>()
                .AddResolver<PropertyResolver, Property>();
        }
    }
}
