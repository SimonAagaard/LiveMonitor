﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAll();
        Task<T> Get(Guid id);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> Get(Expression<Func<T, bool>> predicate, string[] children);
        Task<List<T>> GetMany(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetMany(Expression<Func<T, bool>> predicate, string[] children);
        Task Add(T entity);
        Task AddMany(List<T> entities);
        Task Update(T entity);
        Task Delete(T entity);
        Task Seed(T entity);

    }
}