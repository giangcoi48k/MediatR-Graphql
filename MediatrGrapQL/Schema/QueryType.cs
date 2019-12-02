using HotChocolate.Types;
using System.Collections.Generic;
using MediatrGrapQL.Attributes;
using MediatrGrapQL.Models;
using MediatrGrapQL.Resolvers;
using MediatrGrapQL.Schema.QueryTypes;
using HotChocolate.Types.Relay;

namespace MediatrGrapQL.Schema
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

            descriptor
                .Field("strings")
                .Type<ListType<PropertyType>>()
                .AddResolver<StringsResolver, PageableData<Property>>();
        }
    }
}
