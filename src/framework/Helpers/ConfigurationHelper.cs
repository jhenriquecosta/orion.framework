using Microsoft.Extensions.Configuration;
using System.IO;

namespace Orion.Framework.Helpers
{
    public class FileConfigurationHelper
    {
        private static IConfigurationRoot xt_configuration;
        private static IConfigurationSection xt_section;
        public static IConfigurationRoot GetConfiguration()
        {
            if (xt_configuration == null)
            {
                xt_configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            }
            return xt_configuration;
        }

        public static string GetStringConnection(string connectionString = "DefaultConnection")
        {
            return GetConfiguration().GetConnectionString(connectionString);
        }
        public static IConfigurationSection GetSection(string _section)
        {
            if (xt_section == null)
            {
                xt_section = GetConfiguration().GetSection(_section);
            }
            return xt_section;           
        }
        public static string GetValue(string key,string section)
        {
            return GetSection(section).GetValue<string>(key);
        }
    }
}
