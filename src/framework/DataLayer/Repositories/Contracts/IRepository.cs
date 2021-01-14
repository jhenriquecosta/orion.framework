using Orion.Framework.Dependency;
using Orion.Framework.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.Repositories.Contracts
{

    public interface IRepository : ITransientDependency
    {

    }
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class,IEntity<TEntity,int>
    { 

    }
    /// This interface is implemented by all repositories to ensure implementation of fixed methods.
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public interface IRepository<TEntity, TKey> : IRepository where TEntity : class,  IEntity<TEntity, TKey>
    {
        /// <summary>
        ///     Used to get a IQueryable that is used to retrieve entities from entire table.
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        IQueryable<TEntity> AsQueryable();


        /// <summary>
        ///     Used to get a IQueryable that is used to retrieve entities from entire table.
        ///     One or more
        /// </summary>
        /// <param name="propertySelectors">A list of include expressions.</param>
        /// <returns>IQueryable to be used to select entities from database</returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);

            /// <summary>
            ///     Used to get all entities.
            /// </summary>
            /// <returns>List of all entities</returns>
            List<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null);

            /// <summary>
            ///     Used to get all entities. entities based on given <paramref name="filter" />.
            /// </summary>
            /// <returns>List of all entities</returns>
            Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null);

           

            /// <summary>
            ///     Used to run a query over entire entities.
            ///     <see cref="UnitOfWorkAttribute" /> attribute is not always necessary (as opposite to <see cref="GetAll" />)
            ///     if <paramref name="queryMethod" /> finishes IQueryable with ToList, FirstOrDefault etc..
            /// </summary>
            /// <typeparam name="T">Type of return value of this method</typeparam>
            /// <param name="queryMethod">This method is used to query over entities</param>
            /// <returns>Query result</returns>
            T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);

            /// <summary>
            ///     Gets an entity with given primary key.
            /// </summary>
            /// <param name="id">Primary key of the entity to get</param>
            /// <returns>Entity</returns>
            TEntity Get(TKey id);

            /// <summary>
            ///     Gets an entity with given primary key.
            /// </summary>
            /// <param name="id">Primary key of the entity to get</param>
            /// <returns>Entity</returns>
            Task<TEntity> GetAsync(TKey id);

            /// <summary>
            ///     Gets exactly one entity with given predicate.
            ///     Throws exception if no entity or more than one entity.
            /// </summary>
            /// <param name="predicate">Entity</param>
            TEntity Single(Expression<Func<TEntity, bool>> filter);

            /// <summary>
            ///     Gets exactly one entity with given predicate.
            ///     Throws exception if no entity or more than one entity.
            /// </summary>
            /// <param name="predicate">Entity</param>
            Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> filter);

            /// <summary>
            ///     Gets an entity with given primary key or null if not found.
            /// </summary>
            /// <param name="id">Primary key of the entity to get</param>
            /// <returns>Entity or null</returns>
            TEntity FirstOrDefault(TKey id);

            /// <summary>
            ///     Gets an entity with given primary key or null if not found.
            /// </summary>
            /// <param name="id">Primary key of the entity to get</param>
            /// <returns>Entity or null</returns>
            Task<TEntity> FirstOrDefaultAsync(TKey id);

            /// <summary>
            ///     Gets an entity with given given predicate or null if not found.
            /// </summary>
            /// <param name="predicate">Predicate to filter entities</param>
            TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter);

            /// <summary>
            ///     Gets an entity with given given predicate or null if not found.
            /// </summary>
            /// <param name="predicate">Predicate to filter entities</param>
            Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

            /// <summary>
            ///     Creates an entity with given primary key without database access.
            /// </summary>
            /// <param name="id">Primary key of the entity to load</param>
            /// <returns>Entity</returns>
            TEntity Load(TKey id);

            /// <summary>
            ///     Inserts a new entity.
            /// </summary>
            /// <param name="entity">Inserted entity</param>
            TEntity Insert(TEntity entity);

            /// <summary>
            ///     Inserts a new entity.
            /// </summary>
            /// <param name="entity">Inserted entity</param>
            Task<TEntity> InsertAsync(TEntity entity);

            /// <summary>
            ///     Inserts a new entity and gets it's Id.
            ///     It may require to save current unit of work
            ///     to be able to retrieve id.
            /// </summary>
            /// <param name="entity">Entity</param>
            /// <returns>Id of the entity</returns>
            TKey InsertAndGetId(TEntity entity);

            /// <summary>
            ///     Inserts a new entity and gets it's Id.
            ///     It may require to save current unit of work
            ///     to be able to retrieve id.
            /// </summary>
            /// <param name="entity">Entity</param>
            /// <returns>Id of the entity</returns>
            Task<TKey> InsertAndGetIdAsync(TEntity entity);

            /// <summary>
            ///     Inserts or updates given entity depending on Id's value.
            /// </summary>
            /// <param name="entity">Entity</param>
            TEntity InsertOrUpdate(TEntity entity);

            /// <summary>
            ///     Inserts or updates given entity depending on Id's value.
            /// </summary>
            /// <param name="entity">Entity</param>
            Task<TEntity> InsertOrUpdateAsync(TEntity entity);

            /// <summary>
            ///     Inserts or updates given entity depending on Id's value.
            ///     Also returns Id of the entity.
            ///     It may require to save current unit of work
            ///     to be able to retrieve id.
            /// </summary>
            /// <param name="entity">Entity</param>
            /// <returns>Id of the entity</returns>
            TKey InsertOrUpdateAndGetId(TEntity entity);

            /// <summary>
            ///     Inserts or updates given entity depending on Id's value.
            ///     Also returns Id of the entity.
            ///     It may require to save current unit of work
            ///     to be able to retrieve id.
            /// </summary>
            /// <param name="entity">Entity</param>
            /// <returns>Id of the entity</returns>
            Task<TKey> InsertOrUpdateAndGetIdAsync(TEntity entity);

            /// <summary>
            ///     Updates an existing entity.
            /// </summary>
            /// <param name="entity">Entity</param>
            TEntity Update(TEntity entity);

            /// <summary>
            ///     Updates an existing entity.
            /// </summary>
            /// <param name="entity">Entity</param>
            Task<TEntity> UpdateAsync(TEntity entity);

            /// <summary>
            ///     Updates an existing entity.
            /// </summary>
            /// <param name="id">Id of the entity</param>
            /// <param name="updateAction">Action that can be used to change values of the entity</param>
            /// <returns>Updated entity</returns>
            TEntity Update(TKey id, Action<TEntity> updateAction);

            /// <summary>
            ///     Updates an existing entity.
            /// </summary>
            /// <param name="id">Id of the entity</param>
            /// <param name="updateAction">Action that can be used to change values of the entity</param>
            /// <returns>Updated entity</returns>
            Task<TEntity> UpdateAsync(TKey id, Func<TEntity, Task> updateAction);

            /// <summary>
            ///     Deletes an entity.
            /// </summary>
            /// <param name="entity">Entity to be deleted</param>
            void Delete(TEntity entity);

            /// <summary>
            ///     Deletes an entity.
            /// </summary>
            /// <param name="entity">Entity to be deleted</param>
            Task DeleteAsync(TEntity entity);

            /// <summary>
            ///     Deletes an entity by primary key.
            /// </summary>
            /// <param name="id">Primary key of the entity</param>
            void Delete(TKey id);

            /// <summary>
            ///     Deletes an entity by primary key.
            /// </summary>
            /// <param name="id">Primary key of the entity</param>
            Task DeleteAsync(TKey id);

            /// <summary>
            ///     Deletes many entities by function.
            ///     Notice that: All entities fits to given predicate are retrieved and deleted.
            ///     This may cause major performance problems if there are too many entities with
            ///     given predicate.
            /// </summary>
            /// <param name="predicate">A condition to filter entities</param>
            void Delete(Expression<Func<TEntity, bool>> predicate);

            /// <summary>
            ///     Deletes many entities by function.
            ///     Notice that: All entities fits to given predicate are retrieved and deleted.
            ///     This may cause major performance problems if there are too many entities with
            ///     given predicate.
            /// </summary>
            /// <param name="predicate">A condition to filter entities</param>
            Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

            /// <summary>
            ///     Gets count of all entities in this repository.
            /// </summary>
            /// <returns>Count of entities</returns>
            int Count();

            /// <summary>
            ///     Gets count of all entities in this repository.
            /// </summary>
            /// <returns>Count of entities</returns>
            Task<int> CountAsync();

            /// <summary>
            ///     Gets count of all entities in this repository based on given <paramref name="predicate" />.
            /// </summary>
            /// <param name="predicate">A method to filter count</param>
            /// <returns>Count of entities</returns>
            int Count(Expression<Func<TEntity, bool>> predicate);

            /// <summary>
            ///     Gets count of all entities in this repository based on given <paramref name="predicate" />.
            /// </summary>
            /// <param name="predicate">A method to filter count</param>
            /// <returns>Count of entities</returns>
            Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

            /// <summary>
            ///     Gets count of all entities in this repository (use if expected return value is greather than
            ///     <see cref="int.MaxValue" />.
            /// </summary>
            /// <returns>Count of entities</returns>
            long LongCount();

            /// <summary>
            ///     Gets count of all entities in this repository (use if expected return value is greather than
            ///     <see cref="int.MaxValue" />.
            /// </summary>
            /// <returns>Count of entities</returns>
            Task<long> LongCountAsync();

            /// <summary>
            ///     Gets count of all entities in this repository based on given <paramref name="predicate" />
            ///     (use this overload if expected return value is greather than <see cref="int.MaxValue" />).
            /// </summary>
            /// <param name="predicate">A method to filter count</param>
            /// <returns>Count of entities</returns>
            long LongCount(Expression<Func<TEntity, bool>> predicate);

            /// <summary>
            ///     Gets count of all entities in this repository based on given <paramref name="predicate" />
            ///     (use this overload if expected return value is greather than <see cref="int.MaxValue" />).
            /// </summary>
            /// <param name="predicate">A method to filter count</param>
            /// <returns>Count of entities</returns>
            Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
        }

}
