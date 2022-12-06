﻿using EA.BlogProject.Shared.Data.Abstract;
using EA.BlogProject.Shared.Entities.Abstract;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Shared.Data.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        protected readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;

            //  AsNoTracking işlemi farklı bir yöntem olarak bu şekilde de yapılabilir, yani  sorguların sonuna AsNoTracking() koymak yerine constructor da alttaki gibi kod eklenebilir. 
            //  Bu sorgulara otomatik ekler. 
            // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 

            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await (predicate == null ? _dbSet.CountAsync() : _dbSet.CountAsync(predicate));
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => { _dbSet.Remove(entity); });
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
             query = query.Where(predicate);
           
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<IList<T>> SearchAsync(IList<Expression<Func<T, bool>>> predicates, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            if (predicates.Any())
            {
                var predicateChain = PredicateBuilder.New<T>();   // LinqKit kütüphanesi
                foreach (var predicate in predicates)
                {
                    // predicate1 && predicate2 && predicate3 && predicateN
                    // predicate1 || predicate2 || predicate3 || predicateN
                    predicateChain.Or(predicate);
                }

                query = query.Where(predicateChain);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().ToListAsync();
        }


        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => { _dbSet.Update(entity); });
            return entity;
        }
    }
}
