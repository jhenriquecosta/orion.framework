﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Orion.Framework.DataLayer.Dapper.TableValueParameters;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Dapper.Repositories
{
    public abstract class AbstractDapperRepositoryBase<TEntity, TPrimaryKey> : IDapperRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public abstract IEnumerable<TEntity> Query(string query, object parameters = null);

        public virtual Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            return Task.FromResult(Query(query, parameters));
        }

        public abstract IEnumerable<TAny> Query<TAny>(string query, object parameters = null) where TAny : class;

        public abstract int Execute(string query, object parameters = null);

        public virtual Task<int> ExecuteAsync(string query, object parameters = null)
        {
            return Task.FromResult(Execute(query, parameters));
        }

        public virtual Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null) where TAny : class
        {
            return Task.FromResult(Query<TAny>(query, parameters));
        }

        public abstract int Count(Expression<Func<TEntity, bool>> predicate);

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public abstract IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true);

        public virtual Task<IEnumerable<TEntity>> GetSetAsync(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            return Task.FromResult(GetSet(predicate, firstResult, maxResults, sortingProperty, ascending));
        }

        public abstract IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression);

        public virtual Task<IEnumerable<TEntity>> GetSetAsync(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            return Task.FromResult(GetSet(predicate, firstResult, maxResults, ascending, sortingExpression));
        }

        public abstract void Insert(TEntity entity);

        public virtual Task InsertAsync(TEntity entity)
        {
            Insert(entity);
            return Task.FromResult(0);
        }

        public abstract void Update(TEntity entity);

        public virtual Task UpdateAsync(TEntity entity)
        {
            Update(entity);
            return Task.FromResult(0);
        }

        public abstract void Delete(TEntity entity);

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public abstract void Delete(Expression<Func<TEntity, bool>> predicate);

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }

        public abstract TPrimaryKey InsertAndGetId(TEntity entity);

        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        public abstract TEntity Get(TPrimaryKey id);

        public virtual Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return Task.FromResult(Get(id));
        }

        public abstract TEntity Single(TPrimaryKey id);

        public abstract IEnumerable<TEntity> GetAll();

        public virtual Task<TEntity> SingleAsync(TPrimaryKey id)
        {
            return Task.FromResult(Single(id));
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public abstract IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAll(predicate));
        }

        public virtual Task<IEnumerable<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            return Task.FromResult(GetAllPaged(predicate, pageNumber, itemsPerPage, sortingProperty, ascending));
        }

        public abstract IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true);

        public virtual Task<IEnumerable<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            return Task.FromResult(GetAllPaged(predicate, pageNumber, itemsPerPage, ascending, sortingExpression));
        }

        public abstract IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression);

        public abstract TEntity Single(Expression<Func<TEntity, bool>> predicate);

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public abstract TEntity FirstOrDefault(TPrimaryKey id);

        public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        public abstract TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        public abstract IEnumerable<TAny> Query<TAny>(string query, TableValueParameter tableValueParameter, CommandType commandType) where TAny : class;

        public abstract int Execute(string query, TableValueParameter tableValueParameter, CommandType commandType);

        public virtual Task<int> ExecuteAsync(string query, TableValueParameter tableValueParameter, CommandType commandType)
        {
            return Task.FromResult(Execute(query, tableValueParameter, commandType));
        }

        public virtual Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, TableValueParameter tableValueParameter, CommandType commandType) where TAny : class
        {
            return Task.FromResult(Query<TAny>(query, tableValueParameter, commandType));
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            ParameterExpression lambdaParam = Expression.Parameter(typeof(TEntity));

            BinaryExpression lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
            );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
