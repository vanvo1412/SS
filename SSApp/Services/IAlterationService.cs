using SSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SSApp.Services
{
    public interface IAlterationService
    {
        Task<List<Alteration>> ListAsync();
        Task<Alteration> DetailAsync(int? id);
        Task<int> CreateAsync(Alteration alteration);
        Task<int> UpdateAsync(Alteration alteration);
        Task<int> DeleteAsync(int id);
        bool Any(Expression<Func<Alteration, bool>> predicate);
    }
}