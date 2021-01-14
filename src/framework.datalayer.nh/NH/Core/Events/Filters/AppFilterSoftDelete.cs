using FluentNHibernate.Mapping;
using Orion.Framework.Settings;
using NHibernate;

namespace Orion.Framework.DataLayer.NH.Events.Filters
{
    internal class AppFilterSoftDelete : FilterDefinition
    {
        public AppFilterSoftDelete()
        {
            //Where IsDeleted != true AND InstitutionCode = :instCode
              WithName(XTConstants.Filters.SoftDelete)
             .WithCondition($"{XTConstants.Filters.SoftDeletePropertyName} != :{XTConstants.Filters.SoftDeleteParamName}")
             .AddParameter(XTConstants.Filters.SoftDeleteParamName, NHibernateUtil.Boolean);
        }
    }
}
