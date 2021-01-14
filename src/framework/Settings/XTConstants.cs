using System;
using System.Collections.Generic;
using System.IO;
using Orion.Framework.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Orion.Framework.Settings
{
    public static class XTConstants
    {

    
        public static class Orms
        {
            public const string Dapper = "Dapper";
            public const string EntityFramework = "EntityFramework";
            public const string EntityFrameworkCore = "EntityFrameworkCore";
            public const string NHibernate = "NHibernate";
        }

        public static class Events
        {
            public const string CausationId = "CausationId";
            public const string UserId = "UserId";
            public const string CorrelationId = "CorrelationId";
            public const string SourceType = "SourceType";
            public const string QualifiedName = "QualifiedName";
            public const string AggregateId = "AggregateId";
        }


        public const string SESSION_CONTEXT_DEFAULT = "Default";

        public const int INST_DEFAULT_CODE = 0;
        public const string FIELD_SETTINGS = "settings";

        public const string ICON_DEFAULT_SISTEMA = "mdi mdi-view-grid-plus-outline mdi-18px";
        public const string ICON_DEFAULT_APLICACAO = "mdi mdi-monitor-dashboard mdi-18px";
        public const string ICON_DEFAULT_MODULO = "mdi mdi-folder-multiple mdi-18px";
        public const string ICON_DEFAULT_ROTINA = "mdi mdi-cog-counterclockwise mdi-18px";


        /// <summary>
        /// The property used to mark objects as deleted
        /// </summary>
        /// 
       
       

        public const string OrganizationFilterName = "OrganizationFilter";       
        public const string OrganizationCodePropertyName = "OrganizationCode";
        public const string OrganizationCodeQueryParamName = "orgCode";

        public const string AppSettingsSessionContext = "SessionContext";
        public static class Filters
        {
            public const string SoftDelete = "SoftDeleteFilter";
            public const string SoftDeleteParamName = "deleted";
            public const string SoftDeletePropertyName = "IsDeleted";
        }
    }

}
