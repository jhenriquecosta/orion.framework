using Orion.Framework.DataLayer.NH;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.Dependency;
using NHibernate.Cfg;

namespace Orion.Framework.DataLayer.NH.Dependency
{
    public interface IApplicationDataContext : IScopeDependency
    {

        IApplicationDataContext DBReset();
        IApplicationDataContext DBUpdate();
    }
    public class ApplicationDataContext : IApplicationDataContext
    {
        private static Configuration _configuration;
        public ApplicationDataContext(INHibernateConfiguration hibernateConfiguration)
        {
            _configuration = hibernateConfiguration.GetConfiguration();
        }
      

    
        public IApplicationDataContext DBReset()
        {
            _configuration.DBReset();
            return this;
        }

        public IApplicationDataContext DBUpdate()
        {
            _configuration.DBUpdate();
            return this;
        }
    }


}
