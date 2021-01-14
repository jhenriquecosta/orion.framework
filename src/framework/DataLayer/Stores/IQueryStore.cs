using System;
using Orion.Framework.DataLayer.Stores.Operations;
using Orion.Framework.Dependency;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores {

    public interface IQueryStore<TEntity> : IQueryStore<TEntity, int>
        where TEntity : class, IKey<int> {
    }

    public interface IQueryStore<TEntity, in TKey> : IScopeDependency,
            IFindQueryable<TEntity, TKey>,
            IFindById<TEntity, TKey>,
            IFindByIdAsync<TEntity, TKey>,
            IFindByIds<TEntity, TKey>,
            IFindByIdsAsync<TEntity, TKey>,
            IFindByIdNoTracking<TEntity, TKey>,
            IFindByIdNoTrackingAsync<TEntity, TKey>,
            IFindByIdsNoTracking<TEntity, TKey>,
            IFindByIdsNoTrackingAsync<TEntity, TKey>,
            ISingle<TEntity, TKey>,
            ISingleAsync<TEntity, TKey>,
            IFindAll<TEntity, TKey>,
            IFindAllAsync<TEntity, TKey>,
            IFindAllNoTracking<TEntity, TKey>,
            IFindAllNoTrackingAsync<TEntity, TKey>,
            IExists<TEntity, TKey>,
            IExistsAsync<TEntity, TKey>,
            IExistsByExpression<TEntity, TKey>,
            IExistsByExpressionAsync<TEntity, TKey>,
            ICount<TEntity, TKey>,
            ICountAsync<TEntity, TKey>,
            IPageQuery<TEntity, TKey>,
            IPageQueryAsync<TEntity, TKey>,
            INHibernateOperations<TEntity,TKey>

           //IGetAllQueryable<TEntity, TKey>,
           //IGet<TEntity, TKey>,
           //IGetAsync<TEntity, TKey>,
           //IGetByIds<TEntity, TKey>,
           //IGetByIdsAsync<TEntity, TKey>,          
           //IGetFirstOrDefault<TEntity, TKey>,
           //IGetFirstOrDefaultAsync<TEntity, TKey>,
           //IGetAllList<TEntity, TKey>,
           //IGetAllListAsync<TEntity, TKey>,
           //IFindAllNoTracking<TEntity, TKey>,
           //IFindAllNoTrackingAsync<TEntity, TKey>,
           //IExists<TEntity, TKey>,
           //IExistsAsync<TEntity, TKey>,
           //IExistsByExpression<TEntity, TKey>,
           //IExistsByExpressionAsync<TEntity, TKey>,
           //ICount<TEntity, TKey>,
           //ICountAsync<TEntity, TKey>,
           //IPageQuery<TEntity, TKey>,
           //IPageQueryAsync<TEntity, TKey>,
           //IFindByIdNoTracking<TEntity, TKey>, //compatibilidade
           //IGetNoTrackingAsync<TEntity, TKey>,
           //IFindByIdsNoTracking<TEntity, TKey>,
           //IFindByIdsNoTrackingAsync<TEntity, TKey>
           where TEntity : class, IKey<TKey>
    {
    }
    //public interface IQueryStore<TEntity, in TKey> : IScopeDependency,
    //    IFindQueryable<TEntity, TKey>,
    //    IFindById<TEntity, TKey>,
    //    IFindByIdAsync<TEntity, TKey>,
    //    IFindAll<TEntity, TKey>,
    //    IFindAllAsync<TEntity, TKey>,
    //    ICount<TEntity, TKey>,
    //    IExists<TEntity, TKey>,
    //    IExistsAsync<TEntity, TKey>,
    //    IExistsByExpression<TEntity, TKey>,
    //    IExistsByExpressionAsync<TEntity, TKey>,
    //    IPageQuery<TEntity, TKey>
    //    where TEntity : class, IKey<TKey> {
    //}
}
