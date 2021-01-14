using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.DataLayer;
using Orion.Framework.Domains;

namespace Orion.Framework.MultiTenancy.DataLayer.CoreDAO
{
    public interface ICoreGridPagingDAO<T> : ICoreGridPagingDAO<T, int> where T : IEntity<int>, IKey<int>
    {
    }
    public interface ICoreGridPagingDAO<T, idT> where T : IEntity<idT>, IKey<idT> where idT : IEquatable<idT>
    {
        RetrievedData<T> RetrieveUsingPaging(IQueryable<T> theQueryOver, int startIndex, int maxRows, bool hasOrderBy = false);
        Task<RetrievedData<T>> RetrieveUsingPagingAsync(IQueryable<T> theQueryOver, int startIndex, int maxRows, bool hasOrderBy = false, CancellationToken token = default(CancellationToken));
        RetrievedData<TTransform> RetrieveUsingPaging<TTransform>(IQueryable<T> theQueryOver, int startIndex, int maxRows, bool hasOrderBy = false)
            where TTransform : class;
        Task<RetrievedData<TTransform>> RetrieveUsingPagingAsync<TTransform>(IQueryable<T> theQueryOver, int startIndex, int maxRows, bool hasOrderBy = false, CancellationToken token = default(CancellationToken))
            where TTransform : class;
    }

}
