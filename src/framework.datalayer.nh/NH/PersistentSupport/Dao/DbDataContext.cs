using NHibernate;

namespace Orion.Framework.DataLayer.NH.Dao
{

    public class DbDataContext : NhDao
    {
        public DbDataContext(ISession factoryProvider) : base(factoryProvider)
        { 

        }
    }
}
