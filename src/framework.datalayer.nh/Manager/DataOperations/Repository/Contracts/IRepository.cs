using Framework.DataLayer.NHibernate.Domains;
using Framework.DataLayer.NHibernate.DataOperation;

namespace Framework.DataLayer.NHibernate.Repository.Contracts
{
    public interface IRepository<TEntity> : IDataOperation<TEntity> where TEntity: class
    {
      
    }
}
