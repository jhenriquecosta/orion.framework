using System;

using NHibernate;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.Repositories.Contracts;
using Orion.Framework.Domains;
using Orion.Framework.Helpers;

namespace Orion.Framework
{
    public static class NhRepositoryExtensions
    {
        public static ISession GetSession<TEntity, TKey>(this IRepository<TEntity, TKey> repository)
            where TEntity : class, IEntity<TEntity,TKey>
        {
            if (ProxyHelper.UnProxyOrSelf(repository) is IRepositoryWithSession repositoryWithSession)
            {
                return repositoryWithSession.GetSession();
            }

            throw new ArgumentException("Given repository does not implement IRepositoryWithSession", nameof(repository));
        }
    }
}
