using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Repository : IRepository
    {
        [ThreadStatic]
        private static DefaultContext _context;
        protected static DefaultContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new DefaultContext();
                }
                return _context;
            }
        }

        public Repository()
        {

        }

        public T GetById<T>(int id) where T : Base
        {
            return Context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : Base
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public T GetSingleBySpec<T>(ISpecification<T> spec) where T : Base
        {
            return List(spec).FirstOrDefault();
        }

        public async Task<T> GetSingleBySpecAsync<T>(ISpecification<T> spec) where T : Base
        {
            return (await ListAsync(spec)).FirstOrDefault();
        }

        public List<T> ListAll<T>(int take = 0, int skip = 0) where T : Base
        {
            var query = Context.Set<T>();

            if ((skip > 0) && (take > 0))
                return query.OrderBy(f => f.Id).Skip(skip).Take(take).ToList();
            if (skip > 0)
                return query.OrderBy(f => f.Id).Skip(skip).ToList();
            if (take > 0)
                return query.OrderBy(f => f.Id).Take(take).ToList();
            return query.ToList();
        }

        public async Task<List<T>> ListAllAsync<T>(int take = 0, int skip = 0) where T : Base
        {
            var query = Context.Set<T>();

            if ((skip > 0) && (take > 0))
                return await query.OrderBy(f => f.Id).Skip(skip).Take(take).ToListAsync();
            if (skip > 0)
                return await query.OrderBy(f => f.Id).Skip(skip).ToListAsync();
            if (take > 0)
                return await query.OrderBy(f => f.Id).Take(take).ToListAsync();
            return await query.ToListAsync();
        }

        public List<T> List<T>(ISpecification<T> spec, int take = 0, int skip = 0) where T : Base
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(Context.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            var query = secondaryResult.Where(spec.Criteria);

            if ((skip > 0) && (take > 0))
                return query.OrderBy(f => f.Id).Skip(skip).Take(take).ToList();
            if (skip > 0)
                return query.OrderBy(f => f.Id).Skip(skip).ToList();
            if (take > 0)
                return query.OrderBy(f => f.Id).Take(take).ToList();
            return query.ToList();
        }

        public async Task<List<T>> ListAsync<T>(ISpecification<T> spec, int take = 0, int skip = 0) where T : Base
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(Context.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            var query = secondaryResult.Where(spec.Criteria);

            if ((skip > 0) && (take > 0))
                return await query.OrderBy(f => f.Id).Skip(skip).Take(take).ToListAsync();
            if (skip > 0)
                return await query.OrderBy(f => f.Id).Skip(skip).ToListAsync();
            if (take > 0)
                return await query.OrderBy(f => f.Id).Take(take).ToListAsync();
            return await query.ToListAsync();
        }

        public T Add<T>(T entity) where T : Base
        {
            Context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync<T>(T entity) where T : Base
        {
            await Task.Factory.StartNew(() => Context.Set<T>().Add(entity));
            return entity;
        }

        public void Update<T>(T entity) where T : Base
        {
            //entity.LastModificationDate = DateTime.Now;
            Context.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync<T>(T entity) where T : Base
        {
            //entity.LastModificationDate = DateTime.Now;
            await Task.Factory.StartNew(() => Context.Entry(entity).State = EntityState.Modified);
        }

        public void Delete<T>(T entity) where T : Base
        {
            Context.Set<T>().Remove(entity);
        }

        public async Task DeleteAsync<T>(T entity) where T : Base
        {
            await Task.Factory.StartNew(() => Context.Set<T>().Remove(entity));
        }

        public void SaveChages<T>() where T : Base
        {
            Context.SaveChanges();
        }

        public async Task SaveChagesAsync<T>() where T : Base
        {
            await Context.SaveChangesAsync();
        }

        public void ReloadEntity<T>(T entity) where T : Base
        {
            Context.Entry(entity).Reload();
        }

        public async Task ReloadEntityAsync<T>(T entity) where T : Base
        {
            await Context.Entry(entity).ReloadAsync();
        }
    }
}
