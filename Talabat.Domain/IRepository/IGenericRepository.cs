using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.Specifications;

namespace Talabat.Domain.IRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAll();

        Task<T> GetById(int id);

        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);

        Task<T> GetByIdwithSpec(ISpecification<T> spec);
        Task<int> GetCountwithSpecAsync(ISpecification<T> spec);
    }
}
