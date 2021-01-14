using Orion.Framework.DataLayer.Repositories.Contracts;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.NH.Applications.Dao
{
    public class DbDao<TEntity> : DaoDomainService<TEntity> where TEntity : class, IEntity<TEntity, int>
    {

        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public DbDao(IRepository<TEntity> repository, IUnitOfWorkManager unitOfWorkManager) : base(repository, unitOfWorkManager)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }
    }
}
