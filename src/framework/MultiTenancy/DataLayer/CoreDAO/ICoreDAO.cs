using System;
using Orion.Framework.Domains;

namespace Orion.Framework.MultiTenancy.DataLayer.CoreDAO
{

    /// <summary>
    /// This holds the minimum data requirements for each entity. It does not include list retrievals and SQL bulk inserts
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICoreDAO<T> : ICoreDAO<T, int> where T : IEntity<int>, IKey<int>
    {

    }

    /// <summary>
    /// This holds the minimum data requirements for each entity. It does not include list retrievals and SQL bulk inserts
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="idT"></typeparam>
    public interface ICoreDAO<T, idT> : ICoreReadsDAO<T, idT>, ICoreGridPagingDAO<T, idT>, ICoreBulkInsertDAO<T, idT>, ICoreWritesDAO<T, idT> where T : IEntity<idT>, IKey<idT> where idT : IEquatable<idT>
    {

    }

    
}
