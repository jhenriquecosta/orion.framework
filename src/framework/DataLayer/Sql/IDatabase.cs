using System.Data;
using Orion.Framework.Aspects;

namespace Orion.Framework.DataLayer.Sql {
   
    [Ignore]
    public interface IDatabase {
       
        IDbConnection GetConnection();
    }
}
