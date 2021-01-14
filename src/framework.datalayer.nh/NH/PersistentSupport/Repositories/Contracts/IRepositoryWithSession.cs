using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.DataLayer.NH.Contracts
{
    public interface IRepositoryWithSession
    {
        ISession GetSession();
    }
}
