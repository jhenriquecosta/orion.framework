using System;
using System.Collections.Generic;
using System.Text;
using Orion.Framework.Applications.Security;
using Orion.Framework.Domains;

namespace Orion.Framework.Applications
{
   public interface ISystemSetting : IAmHostedCentrally
    {
        string ApplicationName { get; set; }
        bool EmailLogMessages { get; set; }
        UsernameAndPasswordRule UsernameAndPasswordRule { get; set; }  
        EmailAndSmtpSetting EmailAndSmtpSetting { get; set; }  

        /// <summary>
        /// The folder where institutions' logo images will be stored
        /// </summary>
       string LogoImageFolder { get; set; }

    }
}
