using System;
using Orion.Framework.Domains;

namespace Orion.Framework.MultiTenancy.DataLayer.CoreDAO
{

    public interface ICoreWritesDAO<T> : ICoreWritesDAO<T, int> where T : IEntity<int>, IKey<int>
    {

    }
    public interface ICoreWritesDAO<T,idT> : ICoreGeneralDAO, ICoreUnitOfWorkDAO where T : IEntity<idT>, IKey<idT> 
    {
        void Refresh(T obj);

        /// <summary>
        /// Updates the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void Update(T obj);

        /// <summary>
        /// Saves the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void Save(T obj);

        void SaveOrUpdate(T obj);

        /// <summary>
        /// Deletes the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void Delete(T obj);

        /// <summary>
        /// Actually deletes the specified obj from the db.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void TakeOutPermanently(T obj);

        T Merge(T entity);

        void Evict(T entity);

    }

  
}
