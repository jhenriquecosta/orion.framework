using System;

using NHibernate;
using Orion.Framework.DataLayer.NH.UnitOfWorks;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;

namespace Orion.Framework
{
    internal static class UnitOfWorkExtensions
    {
        public static ISession GetSession<TSessionContext>(this IActiveUnitOfWork unitOfWork) where TSessionContext : ISessionContext
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            if (!(unitOfWork is NhUnitOfWork)) throw new ArgumentException("unitOfWork is not type of " + typeof(NhUnitOfWork).FullName, nameof(unitOfWork));

            return ((NhUnitOfWork)unitOfWork).GetOrCreateSession<TSessionContext>();
        }

    }
}
