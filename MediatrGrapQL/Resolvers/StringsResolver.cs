using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotChocolate.Types.Relay;
using System.Linq;
using MediatrGrapQL.Models;
using MediatrGrapQL.Schema.QueryTypes;
using System;

namespace MediatrGrapQL.Resolvers
{
    public sealed class StringsResolver : IBaseResolver<PageableData<Property>>
    {
        private IResolverContext _context;

        public bool Descending { get; set; }



        public void ConfigFieldDescriptor(IObjectFieldDescriptor descriptor)
        {
            descriptor
                .Argument("descending", a => a.Type<BooleanType>())
                .UsePaging<PropertyType>();
        }

        public void ResolverContext(IResolverContext context)
        {
            _context = context;
            Descending = context.Argument<bool>("descending");
        }

        private class Handler : IBaseResolverHandler<StringsResolver, PageableData<Property>>
        {
            private readonly ApplicationDbContext _dbContext;

            public Handler(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<PageableData<Property>> Handle(StringsResolver request, CancellationToken cancellationToken)
            {
                var ctx = request._context;

                IDictionary<string, object> cursorProperties = ctx.GetCursorProperties();

                // get the sort order from the sorting argument or from a cursor that was passed in.
                bool descending = cursorProperties.TryGetValue("descending", out object d)
                    ? (bool)d
                    : request.Descending;

                // set the cursor sorting property.
                cursorProperties["descending"] = descending;

                // return the sorted string dataset with the cursor properties.
                var result = descending
                    ? new PageableData<Property>(_dbContext.Properties.OrderByDescending(t => t.Id), cursorProperties)
                    : new PageableData<Property>(_dbContext.Properties, cursorProperties);

                return await Task.FromResult(result);
            }
        }
    }
}
