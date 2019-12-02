using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatrGrapQL.Models;
using MediatrGrapQL.Services;

namespace MediatrGrapQL.Resolvers
{
    public sealed class PropertiesResolver : IBaseResolver<IEnumerable<Property>>
    {
        public int? Last { get; set; }

        public void ConfigFieldDescriptor(IObjectFieldDescriptor descriptor)
        {
            descriptor.Argument("last", a => a.Type<IdType>());
        }

        public void ResolverContext(IResolverContext context)
        {
            Last = context.Argument<int?>("last");
        }

        private class Handler : IBaseResolverHandler<PropertiesResolver, IEnumerable<Property>>
        {
            private readonly PropertyService _propertyService;

            public Handler(PropertyService propertyService)
            {
                _propertyService = propertyService;
            }

            public async Task<IEnumerable<Property>> Handle(PropertiesResolver request, CancellationToken cancellationToken)
            {
                return await _propertyService.GetAllAsync(request.Last);
            }
        }
    }
}
