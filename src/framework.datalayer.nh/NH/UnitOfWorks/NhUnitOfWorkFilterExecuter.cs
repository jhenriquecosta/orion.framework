﻿
using NHibernate;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;

namespace Orion.Framework.DataLayer.NH.UnitOfWorks
{
    public class NhUnitOfWorkFilterExecuter : IUnitOfWorkFilterExecuter
    {
        public void ApplyDisableFilter(IUnitOfWork unitOfWork, string filterName)
        {
            ISession session = unitOfWork.As<NhUnitOfWork>().Session;
            if (session.GetEnabledFilter(filterName) != null)
            {
                session.DisableFilter(filterName);
            }
        }

        public void ApplyEnableFilter(IUnitOfWork unitOfWork, string filterName)
        {
            ISession session = unitOfWork.As<NhUnitOfWork>().Session;
            if (session.GetEnabledFilter(filterName) == null)
            {
                session.EnableFilter(filterName);
            }
        }

        public void ApplyFilterParameterValue(IUnitOfWork unitOfWork, string filterName, string parameterName, object value)
        {
            ISession session = unitOfWork.As<NhUnitOfWork>().Session;
            if (session == null)
            {
                return;
            }

            IFilter filter = session.GetEnabledFilter(filterName);
            if (filter != null)
            {
                filter.SetParameter(parameterName, value);
            }
        }
    }
}
