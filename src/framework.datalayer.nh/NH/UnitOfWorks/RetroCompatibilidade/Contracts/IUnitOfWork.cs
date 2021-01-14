using NHibernate;
using System;
using Orion.Framework.DataLayer.UnitOfWorks;

namespace Orion.Framework.DataLayer.NHibernate.UnitOfWorks.Contracts
{
    public interface IUnitOfWork : IUnitOfWorkBase
    {
        ISession Session { get; }
        IStatelessSession SessionReadOnly { get; }
        Type Entity { get; set; }
    }
}