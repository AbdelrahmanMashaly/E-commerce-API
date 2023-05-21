using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepository;
using Talabat.Domain.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAll()
        {
            if (typeof(T) == typeof(Product))
                return (IReadOnlyList< T >) await  _context.Products.Include(P => P.ProductType).Include(P => P.ProductBrand).ToListAsync();
            else

            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<T> GetByIdwithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<int> GetCountwithSpecAsync(ISpecification<T> spec)
        {
           return await ApplySpecification(spec).CountAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
           var query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>() , spec);
            return query;
        }


    }
}
