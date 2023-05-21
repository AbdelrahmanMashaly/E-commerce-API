using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity  
    {
        public static IQueryable<TEntity> GetQuery( IQueryable<TEntity> inputQuery , ISpecification<TEntity> spec)
        {
            // _context.Products
            var query = inputQuery;

            // _context.Products.Where(P=> P.Id == Id)
            if(spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            if(spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);

            if(spec.IsPaginated)
                query = query.Skip(spec.Skip).Take(spec.Take);

            // _context.Products.Where(P=> P.Id == Id).Include(P=>P.ProductBrand).Include(P=>P.ProductType) ; 
            query = spec.Includes.Aggregate(query , (currentQuery , IncludeExpression) => currentQuery.Include(IncludeExpression));
           
            return query;
        }
    }
}
