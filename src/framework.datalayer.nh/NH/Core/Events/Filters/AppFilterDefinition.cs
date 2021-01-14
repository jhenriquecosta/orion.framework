using FluentNHibernate.Mapping;
using Orion.Framework.Settings;
using NHibernate;

namespace Orion.Framework.DataLayer.NH.Events.Filters
{
    internal class AppFilterDefinition : FilterDefinition
    {
        public AppFilterDefinition()
        {
              //Where IsDeleted != true AND InstitutionCode = :instCode
              WithName(XTConstants.OrganizationFilterName)
             .WithCondition($"{XTConstants.Filters.SoftDeletePropertyName} != :{XTConstants.Filters.SoftDeleteParamName} AND {XTConstants.OrganizationCodePropertyName} = :{XTConstants.OrganizationCodeQueryParamName}")
             .AddParameter(XTConstants.OrganizationCodeQueryParamName, NHibernateUtil.Int32)
             .AddParameter(XTConstants.Filters.SoftDeleteParamName, NHibernateUtil.Boolean);
           
        }
    }
}
