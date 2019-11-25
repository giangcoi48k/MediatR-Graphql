using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGrapQL.Attributes;
using TestGrapQL.Models;

namespace TestGrapQL.Services
{
    [Injected]
    public class PaymentService
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Payment>> GetAllForPropertyAsync(int propertyId, int? lastAmount)
        {
            var query = _dbContext.Payments.Where(t => t.PropertyId == propertyId)
                .OrderByDescending(t => t.DateCreated)
                .AsQueryable();
            if (lastAmount != null)
                query = query.Take(lastAmount.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllForPropertiesAsync(IReadOnlyList<int> keys, int? lastAmount)
        {
            var query = _dbContext.Payments.Where(t => keys.Contains(t.PropertyId))
                .GroupBy(t => t.PropertyId)
                .SelectMany(t => lastAmount == null
                    ? t.OrderByDescending(x => x.DateCreated)
                    : t.OrderByDescending(x => x.DateCreated).Take(lastAmount.Value)
                );

            return await query.ToListAsync();
        }
    }
}
