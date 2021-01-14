using NHibernate.Cfg;
using FluentNHibernate.Cfg;

namespace Orion.Framework.DataLayer.NH.Contracts
{
    public interface INHibernateConfiguration
    {
        Configuration GetConfiguration( );       
       
    }
}
