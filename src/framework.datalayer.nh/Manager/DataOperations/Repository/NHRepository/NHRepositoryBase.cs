using NHibernate;
using Zeus.Domains;

namespace Zeus.NHibernate.Repository
{
    public abstract class NHRepositoryBase<TEntity> : NHRepository<TEntity>,INHRepository<TEntity> where TEntity : IAggregateRoot
    {
        protected NHRepositoryBase(ISession session) : base(session)
        {

        }
    }
}