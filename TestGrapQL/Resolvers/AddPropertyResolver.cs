using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;
using TestGrapQL.Services;

namespace TestGrapQL.Resolvers
{
    public sealed class AddPropertyResolver : IBaseResolver
    {
        public int Id { get; set; }

        public void AddArguments(IObjectFieldDescriptor descriptor)
        {
            descriptor.Argument("id", a => a.Type<IdType>());
        }

        public void ResolveArguments(IResolverContext context)
        {
            Id = context.Argument<int>("id");
        }

        private class Handler : IBaseResolverHandler<AddPropertyResolver>
        {
            private readonly PropertyService _propertyService;

            public Handler(PropertyService propertyService)
            {
                _propertyService = propertyService;
            }

            public async Task<object> Handle(AddPropertyResolver request, CancellationToken cancellationToken)
            {
                return await _propertyService.GetByIdAsync(request.Id);
            }
        }
    }
}
