using System;
using System.Data;
using System.Threading.Tasks;
using Orion.Framework.Aspects;

namespace Orion.Framework.DataLayer.NHibernate.UnitOfWorks.Contracts
{ 
 
    [Ignore]
    public interface IUnitOfWorkBase : IDisposable 
    {
        string TraceId { get; set; }
        bool IsActiveTransaction { get; }
        bool Disposed { get; }
        void BeginTransaction();
        void CloseTransaction();
        void BeginTransaction(IsolationLevel isolationLevel);
        void Rollback();
        Task RollbackAsync();
        Task CommitAsync();
        void Commit();

    }
}
