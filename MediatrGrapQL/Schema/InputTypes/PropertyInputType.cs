using HotChocolate.Types;
using MediatrGrapQL.Models;

namespace MediatrGrapQL.Schema.InputTypes
{
    public class PropertyInputType : ObjectType<Property>
    {
        protected override void Configure(IObjectTypeDescriptor<Property> descriptor)
        {
            descriptor.Name(nameof(PropertyInputType));
        }
    }
}
