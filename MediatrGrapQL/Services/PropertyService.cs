﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatrGrapQL.Attributes;
using MediatrGrapQL.Models;
using System;
using EFSecondLevelCache.Core;

namespace MediatrGrapQL.Services
{
    [Injected]
    public class PropertyService
    {
        private readonly ApplicationDbContext _dbContext;

        public PropertyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Property>> GetAllAsync(int? last)
        {
            var query = _dbContext.Properties
                .OrderByDescending(t => t.Id)
                .AsQueryable();

            if (last != null)
                query = query.Take(last.Value);

            return await query
                .ToListAsync();
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            return await _dbContext.Properties.FindAsync(id);
        }

        public async Task<Property> AddProperty(Property entity)
        {
            _dbContext.Properties.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
