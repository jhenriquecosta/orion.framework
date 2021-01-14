using FluentNHibernate.Mapping;
using Orion.Framework.Settings;
using NHibernate;

namespace Orion.Framework.DataLayer.NH.Events.Filters
{
    internal class AppFilterOrganization : FilterDefinition
    {
        public AppFilterOrganization()
        {
            //Where IsDeleted != true AND InstitutionCode = :instCode
            WithName(XTConstants.OrganizationFilterName)
           .WithCondition($"{XTConstants.OrganizationCodePropertyName} = :{XTConstants.OrganizationCodeQueryParamName}")
           .AddParameter(XTConstants.OrganizationCodeQueryParamName, NHibernateUtil.Int32);
        }
    }
}
