using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatrGrapQL.Attributes;
using MediatrGrapQL.Models;
using EFSecondLevelCache.Core;
using System;

namespace MediatrGrapQL.Services
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

        public async Task<IEnumerable<Payment>> GetAllForPropertiesAsync(IReadOnlyList<int> keys, int? last)
        {
            var query = _dbContext.Payments.Where(t => keys.Contains(t.PropertyId))
                .OrderByDescending(t => t.DateCreated)
                .AsQueryable();

            if (last != null)
                query = query.GroupBy(t => t.PropertyId).SelectMany(t => t.Take(last.Value));

            var result = await query.ToListAsync();
            return result;
        }
    }
}
