using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.Transactions {
    /// <summary>
    /// 
    /// </summary>
    public class TransactionActionManager : ITransactionActionManager {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<Func<IDbTransaction, Task>> _actions;

        /// <summary>
        /// 
        /// </summary>
        public TransactionActionManager() {
            _actions = new List<Func<IDbTransaction, Task>>();
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count => _actions.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Register( Func<IDbTransaction, Task> action ) {
            if( action == null )
                return;
            _actions.Add( action );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public async Task CommitAsync( IDbTransaction transaction ) {
            foreach( var action in _actions )
                await action( transaction );
            _actions.Clear();
        }
    }
}
