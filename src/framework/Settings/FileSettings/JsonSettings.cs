using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Orion.Framework.Applications
{
    public class JsonSettings : IJsonSettings
    {
        private static IConfigurationSection _section;
        private static IConfigurationRoot _configuration;
        public JsonSettings()
        {
            if (_configuration == null)
            {
                _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            }
        }
        public string GetValue(string section, string value)
        {
            if (section==null) return  string.Empty;
            value = _configuration.GetSection(section).GetValue<string>(value);
            if (value == null) value = string.Empty;
            return value;
        }
        public static string GetStringConnection(string connectionString = "DefaultConnection")
        {
            return _configuration.GetConnectionString(connectionString);
        }
        public static IConfigurationSection GetSection(string _value)
        {
            if (_section == null)
            {
                _section = _configuration.GetSection(_value);
            }
            return _section;
        }
    }
}
