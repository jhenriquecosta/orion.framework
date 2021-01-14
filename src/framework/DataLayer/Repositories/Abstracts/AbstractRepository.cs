using Orion.Framework.DataLayer.Repositories.Contracts;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Domains;
using Orion.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.Repositories.Abstracts
{
    /// <summary>
    ///     Base class to implement <see cref="IRepository{TEntity,TPrimaryKey}" />.
    ///     It implements some methods in most simple way.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public abstract class AbstractRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TEntity, TKey>
    {
        public IUnitOfWorkManager UnitOfWorkManager { get; set; }
        public abstract IQueryable<TEntity> AsQueryable();

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
           return filter == null ? AsQueryable() : AsQueryable().Where(filter);
        }

		public virtual List<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null)
		{
			return GetAll(filter).ToList();
		}
        public virtual Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return Task.FromResult(GetAll(filter).ToList());            
        }
        
	 

		public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
		{
			return queryMethod(GetAll());
		}

		public virtual TEntity Get(TKey id)
		{
			TEntity entity = FirstOrDefault(id);
			if (entity == null)
			{
				throw new EntityNotFoundException(typeof(TEntity), id);
			}

			return entity;
		}

		public virtual async Task<TEntity> GetAsync(TKey id)
		{
			TEntity entity = await FirstOrDefaultAsync(id);
			if (entity == null)
			{
				throw new EntityNotFoundException(typeof(TEntity), id);
			}

			return entity;
		}

		public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
		{
			return GetAll().Single(predicate);
		}

		public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return Task.FromResult(Single(predicate));
		}

		public virtual TEntity FirstOrDefault(TKey id)
		{
			return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
		}

		public virtual Task<TEntity> FirstOrDefaultAsync(TKey id)
		{
			return Task.FromResult(FirstOrDefault(id));
		}

		public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
		{
			return GetAll().FirstOrDefault(predicate);
		}

		public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return Task.FromResult(FirstOrDefault(predicate));
		}

		public virtual TEntity Load(TKey id)
		{
			return Get(id);
		}

		public abstract TEntity Insert(TEntity entity);

		public virtual Task<TEntity> InsertAsync(TEntity entity)
		{
			return Task.FromResult(Insert(entity));
		}

		public virtual TKey InsertAndGetId(TEntity entity)
		{
			return Insert(entity).Id;
		}

		public virtual Task<TKey> InsertAndGetIdAsync(TEntity entity)
		{
			return Task.FromResult(InsertAndGetId(entity));
		}

		public virtual TEntity InsertOrUpdate(TEntity entity)
		{
			return entity.IsTransient()
				? Insert(entity)
				: Update(entity);
		}

		public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
		{
			return entity.IsTransient()
				? await InsertAsync(entity)
				: await UpdateAsync(entity);
		}

		public virtual TKey InsertOrUpdateAndGetId(TEntity entity)
		{
			return InsertOrUpdate(entity).Id;
		}

		public virtual Task<TKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
		{
			return Task.FromResult(InsertOrUpdateAndGetId(entity));
		}

		public abstract TEntity Update(TEntity entity);

		public virtual Task<TEntity> UpdateAsync(TEntity entity)
		{
			return Task.FromResult(Update(entity));
		}

		public virtual TEntity Update(TKey id, Action<TEntity> updateAction)
		{
			TEntity entity = Get(id);
			updateAction(entity);
			return entity;
		}

		public virtual async Task<TEntity> UpdateAsync(TKey id, Func<TEntity, Task> updateAction)
		{
			TEntity entity = await GetAsync(id);
			await updateAction(entity);
			return entity;
		}

		public abstract void Delete(TEntity entity);

		public virtual Task DeleteAsync(TEntity entity)
		{
			Delete(entity);
			return Task.FromResult(0);
		}

		public abstract void Delete(TKey id);

		public virtual Task DeleteAsync(TKey id)
		{
			Delete(id);
			return Task.FromResult(0);
		}

		public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
		{
			foreach (TEntity entity in GetAll().Where(predicate).ToList())
			{
				Delete(entity);
			}
		}

		public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
		{
			Delete(predicate);
			return Task.FromResult(0);
		}

		public virtual int Count()
		{
			return GetAll().Count();
		}

		public virtual Task<int> CountAsync()
		{
			return Task.FromResult(Count());
		}

		public virtual int Count(Expression<Func<TEntity, bool>> predicate)
		{
			return GetAll().Where(predicate).Count();
		}

		public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return Task.FromResult(Count(predicate));
		}

		public virtual long LongCount()
		{
			return GetAll().LongCount();
		}

		public virtual Task<long> LongCountAsync()
		{
			return Task.FromResult(LongCount());
		}

		public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
		{
			return GetAll().Where(predicate).LongCount();
		}

		public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return Task.FromResult(LongCount(predicate));
		}

		

		protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TKey id)
		{
			ParameterExpression lambdaParam = Expression.Parameter(typeof(TEntity));

			BinaryExpression lambdaBody = Expression.Equal(
				Expression.PropertyOrField(lambdaParam, "Id"),
				Expression.Constant(id, typeof(TKey))
			);

			return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
		}
	}
}
