using System;
using System.Data;
using System.Threading.Tasks;
using Orion.Framework.Dependency;

namespace Orion.Framework.DataLayer.Transactions {
   
    public interface ITransactionActionManager : IScopeDependency {
       
        int Count { get; }
      
        void Register( Func<IDbTransaction, Task> action );
     
        Task CommitAsync( IDbTransaction transaction );
    }
}
