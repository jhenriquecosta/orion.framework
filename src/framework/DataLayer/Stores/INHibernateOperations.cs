using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores
{
    public interface INHibernateOperations<TEntity, in TKey> where TEntity : class, IKey<TKey>
    {
        TEntity UnProxy(TEntity entity);
        TEntity AsProxy(object id);

        void Attach(TEntity entity);
        void DeAttach(TEntity entity);
        void Refresh(TEntity entity);

    }
}