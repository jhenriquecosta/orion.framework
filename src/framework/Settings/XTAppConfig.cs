using Microsoft.Extensions.Configuration;

namespace Orion.Framework.Settings
{
    public interface IXTAppConfiguration
    {
        IXTAppConfiguration GetSection(string section);
        T GetValue<T>(string key="");
        string GetStringConnection(string connectionString = "DefaultConnection");
    }
    public class XTAppConfiguration : IXTAppConfiguration
    {
        private readonly IConfiguration _configuration;
        private static IConfigurationSection _rootSection;
        public XTAppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            _rootSection = null;
        }
        public IXTAppConfiguration GetSection(string rootSection)
        {
            if (_rootSection == null)
            {
                _rootSection = _configuration.GetSection(rootSection);
            }
            else
            {
                _rootSection = _rootSection.GetSection(rootSection);
            }
            return this;
        }
        public T GetValue<T>(string valueKey = "")
        {
           if (valueKey.IsNullOrEmpty())
           {
                var result = _rootSection.Get<T>();
                return result;
           }
           else
           {
                var result = _rootSection.GetValue<T>(valueKey);
                return result;
           }
        }
        public  string GetStringConnection(string connectionString = "DefaultConnection")
        {
            return _configuration.GetConnectionString(connectionString);
        }
    }

}
