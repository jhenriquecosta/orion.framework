using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.Domains;
using System.Data.Common;

namespace Orion.Framework.DataLayer.Dapper.Repositories
{
    public class DapperNhRepositoryBase<TSessionContext, TEntity, TPrimaryKey> : DapperRepositoryBase<TEntity, TPrimaryKey>
      where TEntity : class, IEntity<TPrimaryKey>
      where TSessionContext : ISessionContext
    {
        private readonly IActiveTransactionProvider _activeTransactionProvider;

        public DapperNhRepositoryBase(IActiveTransactionProvider activeTransactionProvider) : base(activeTransactionProvider)
        {
            _activeTransactionProvider = activeTransactionProvider;
        }

        public ActiveTransactionProviderArgs ActiveTransactionProviderArgs => new ActiveTransactionProviderArgs
        {
            ["SessionContextType"] = typeof(TSessionContext)
        };

        public override DbConnection Connection => (DbConnection)_activeTransactionProvider.GetActiveConnection(ActiveTransactionProviderArgs);

        public override DbTransaction ActiveTransaction => (DbTransaction)_activeTransactionProvider.GetActiveTransaction(ActiveTransactionProviderArgs);
    }
}
