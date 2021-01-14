using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Framework.DataLayer.NHibernate.Domains.Trees;
using NHibernate.Linq;
using Framework.DataLayer.NHibernate.DataLayer.UnitOfWorks;

namespace Framework.DataLayer.NHibernate.Repository.Base
{
  
    public abstract class TreeRepositoryBase<TEntity> : TreeRepositoryBase<TEntity, Guid, Guid?>, ITreeRepository<TEntity> where TEntity : class, ITreeEntity<TEntity, Guid, Guid?>
    {
        protected TreeRepositoryBase(IUnitOfWorkFactory unitOfWork) : base(unitOfWork)
        {
        }
        public override async Task<int> GenerateSortIdAsync(Guid? parentId)
        {
            var maxSortId = await Find(t => t.ParentId == parentId).MaxAsync(t => t.SortId);
            return maxSortId.SafeValue() + 1;
        }
    }

    public abstract class TreeRepositoryBase<TEntity, TKey, TParentId> : RepositoryBase<TEntity, TKey>, ITreeRepository<TEntity, TKey, TParentId> where TEntity : class, ITreeEntity<TEntity, TKey, TParentId>
    {
        protected TreeRepositoryBase(IUnitOfWorkFactory unitOfWork) : base(unitOfWork)
        {
        }

        public abstract Task<int> GenerateSortIdAsync(TParentId parentId);

        public virtual async Task<List<TEntity>> GetAllChildrenAsync(TEntity parent)
        {
            var list = await FindAllAsync(t => t.Path.StartsWith(parent.Path));
            return list.Where(t => !t.Id.Equals(parent.Id)).ToList();
        }
    }
}
