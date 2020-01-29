using Microsoft.EntityFrameworkCore;
using SSApp.Data;
using SSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SSApp.Services
{
    public class AlterationService : IAlterationService
    {
        private readonly SSAppContext _context;

        public AlterationService(SSAppContext context)
        {
            _context = context;
        }

        public Task<int> CreateAsync(Alteration alteration)
        {
            _context.Add(alteration);
            return _context.SaveChangesAsync();
        }

        public Task<Alteration> DetailAsync(int? id)
        {
            return _context.Alteration
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<List<Alteration>> ListAsync()
        {
            return _context.Alteration.ToListAsync();
        }

        public Task<int> UpdateAsync(Alteration alteration)
        {
            _context.Update(alteration);
            return _context.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(int id)
        {
            var toDelete = _context.Alteration.Find(id);
            _context.Alteration.Remove(toDelete);
            _context.Alteration.Any(e => e.Id == id);
            return _context.SaveChangesAsync();
        }

        public bool Any(Expression<Func<Alteration, bool>> predicate)
        {
            return _context.Alteration.Any(predicate);
        }
    }
}
