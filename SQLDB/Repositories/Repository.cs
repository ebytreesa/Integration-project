using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLDB.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SQLDB.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CustomerDbContext _customerDbContext;
        public Repository(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
            
        }
        public virtual async Task<int> SaveChanges()
        {
            return await _customerDbContext.SaveChangesAsync();
        }



        //public async Task CUD(List<TEntity> entities)
        //{
        //    var dbList = await GetAll();

        //    foreach (var entity in entities)
        //    {
        //        await CUD(entity);
        //        await SaveChanges();

        //    }
        //}

        //public virtual async Task CUD(TEntity entity)
        //{
        //    await SaveChanges();

        //}

        public virtual async Task Add(TEntity entity)
        {
            
            _customerDbContext.Set<TEntity>().Add(entity);
            await SaveChanges();

        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _customerDbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _customerDbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task Remove(TEntity entity)
        {
            _customerDbContext.Remove(entity);
            await SaveChanges();
        }

       

       
        public virtual async Task Update(TEntity entity)
        {
            _customerDbContext.Set<TEntity>().Update(entity);
            await SaveChanges();

        }


        public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await _customerDbContext.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
