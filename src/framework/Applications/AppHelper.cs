using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework.Caches;
using Orion.Framework.Helpers;
using Orion.Framework.Settings;
using Orion.Framework.Utilities;

namespace Orion.Framework
{
    public class AppHelper
    {
        private static IXTAppConfiguration _config;
        private static IServiceCollection _services;
        private static IXTSysSettings _settings;
        private static ICache _cache;
        public static string GetFolder(string[] folders )
        {
            var folder = Path.Combine(folders[0], folders[1]);
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }
            return folder;
        }
        public static void Initialize(IServiceCollection services)
        {
            _services = services;
        }
        public static T Resolve<T>()
        {
            return Ioc.Create<T>();
        }
        public static T GetService<T>()
        {
            return _services.BuildServiceProvider().GetService<T>();
        }
        public static IEnumerable<T> GetServices<T>()
        {
            var result = _services.BuildServiceProvider().GetServices<T>();
            return result;

        }
        public static IXTSysSettings SysSettings()
        {
            if (_settings == null) _settings = _services.BuildServiceProvider().GetService<IXTSysSettings>();
            return _settings;
        }
        public static IXTAppConfiguration AppSettings()
        {
            _config = _services.BuildServiceProvider().GetService<IXTAppConfiguration>();
            return _config;
        }
        public static bool AddCache<T>(string key,T value)
        {
            if (_cache == null) _cache = _services.BuildServiceProvider().GetService<ICache>();
            if (_cache.Exists(key)) _cache.Remove(key);
            var result =_cache.TryAdd<T>(key,value);
            return result;
        }
        public static T GetCache<T>(string key)
        {
            if (_cache == null) _cache = _services.BuildServiceProvider().GetService<ICache>();
            var obj = _cache.Get<T>(key);
            return obj;
        }
       
    }
}
