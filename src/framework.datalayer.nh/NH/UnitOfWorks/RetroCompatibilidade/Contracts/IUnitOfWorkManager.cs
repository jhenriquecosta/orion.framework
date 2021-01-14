using System.Threading.Tasks;
using Orion.Framework.Dependency;

namespace Orion.Framework.DataLayer.NHibernate.UnitOfWorks.Contracts
{ 
 
    public interface IUnitOfWorkManager : IScopeDependency {

     
        void BeginTransaction();
        Task RollbackAsync();
        void Commit();
        Task CommitAsync();
        void Register( IUnitOfWorkBase unitOfWork );
    }
}
