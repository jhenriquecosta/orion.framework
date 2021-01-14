using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.Dependency;

namespace Orion.Framework.MultiTenancy.DataLayer.CoreDAO
{
    public interface ICoreUnitOfWorkDAO : IScopeDependency
    {
        /// <summary>
        /// Commits the changes.
        /// </summary>
        void CommitChanges();

        /// <summary>
        /// Rollbacks the changes.
        /// </summary>
        void RollbackChanges();
        Task CommitChangesAsync(CancellationToken token = default(CancellationToken));
        Task RollbackChangesAsync(CancellationToken token = default(CancellationToken));
    }
}
