using System;
using Orion.Framework.Aspects;

namespace Orion.Framework.DataLayer.Sql.Matedatas {
    
    [Ignore]
    public interface IEntityMatedata {
      
        string GetTable( Type type );
     
        string GetSchema( Type type );
    
        string GetColumn( Type type, string property );
    }
}
