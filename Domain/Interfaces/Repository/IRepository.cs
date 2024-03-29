﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    
        public interface IRepository<TEntity> 
        {
            Task Add(TEntity entity);
            Task<TEntity> GetById(int id);
            Task<List<TEntity>> GetAll();
            Task Update(TEntity entity);
            Task Remove(TEntity entity);
            Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
            Task<int> SaveChanges();
        }
    
}
