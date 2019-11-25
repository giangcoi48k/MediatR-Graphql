using HotChocolate.Types;
using TestGrapQL.Models;

namespace TestGrapQL.Schema.InputTypes
{
    public class PropertyInputType : ObjectType<Property>
    {
        protected override void Configure(IObjectTypeDescriptor<Property> descriptor)
        {
            descriptor.Name(nameof(PropertyInputType));
        }
    }
}
