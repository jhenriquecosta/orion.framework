using System;
using System.Collections.Generic;
using System.Text;
using Orion.Framework.Dependency;

namespace Orion.Framework.Applications
{
    public enum SqlConnectionInfo
    {
        Values,
        Raw
    }
    public enum SqlAuthenticationType
    {
        SQL,
        Windows
    }
    public interface IInstallMode : IScopeDependency
    {


        string AdminEmail { get; set; }



        string AdminPassword { get; set; }



        string ConfirmPassword { get; set; }


        string DatabaseConnectionString { get; set; }


        string DatabaseProvider { get; set; }


        SqlConnectionInfo SqlConnectionInfo { get; set; }



        string SqlServerName { get; set; }



        string SqlDatabaseName { get; set; }



        string SqlServerUsername { get; set; }



        string SqlServerPassword { get; set; }


        SqlAuthenticationType SqlAuthenticationType { get; set; }


        bool SqlServerCreateDatabase { get; set; }


        string SiteName { get; set; }


        string SiteUrl { get; set; }




    }
}
