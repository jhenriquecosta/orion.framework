using System;
using System.Threading;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.UnitOfWorks.Contracts
{
    /// <summary>
    ///     Used to complete a unit of work.
    ///     This interface can not be injected or directly used.
    ///     Use <see cref="IUnitOfWorkManager" /> instead.
    /// </summary>
    public interface IUnitOfWorkCompleteHandle : IDisposable
    {
        /// <summary>
        ///     Completes this unit of work.
        ///     It saves all changes and commit transaction if exists.
        /// </summary>
        void Complete();

        /// <summary>
        ///     Completes this unit of work.
        ///     It saves all changes and commit transaction if exists.
        /// </summary>
        Task CompleteAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
