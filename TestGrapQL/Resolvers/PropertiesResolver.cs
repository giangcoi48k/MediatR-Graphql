using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestGrapQL.Models;
using TestGrapQL.Services;

namespace TestGrapQL.Resolvers
{
    public sealed class PropertiesResolver : IBaseResolver<IEnumerable<Property>>
    {
        private IResolverContext _context;

        public int? Last { get; set; }

        public void AddArguments(IObjectFieldDescriptor descriptor)
        {
            descriptor.Argument("last", a => a.Type<IdType>());
        }

        public void ResolveArguments(IResolverContext context)
        {
            _context = context;
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
                var a = request._context;
                return await _propertyService.GetAllAsync(request.Last);
            }
        }
    }
}
