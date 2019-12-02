using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;
using MediatrGrapQL.Models;
using MediatrGrapQL.Services;

namespace MediatrGrapQL.Resolvers
{
    public sealed class PropertyResolver : IBaseResolver<Property>
    {
        public int Id { get; set; }

        public void ConfigFieldDescriptor(IObjectFieldDescriptor descriptor)
        {
            descriptor.Argument("id", a => a.Type<IdType>());
        }

        public void ResolverContext(IResolverContext context)
        {
            Id = context.Argument<int>("id");
        }

        private class Handler : IBaseResolverHandler<PropertyResolver, Property>
        {
            private readonly PropertyService _propertyService;

            public Handler(PropertyService propertyService)
            {
                _propertyService = propertyService;
            }

            public async Task<Property> Handle(PropertyResolver request, CancellationToken cancellationToken)
            {
                return await _propertyService.GetByIdAsync(request.Id);
            }
        }
    }
}
