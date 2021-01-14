using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.PlatformAbstractions;
using Orion.Framework.Helpers;
using Regex = System.Text.RegularExpressions.Regex;

namespace Orion.Framework.Reflections
{

    public class Finder : IFind
    {
        private const string SkipBlazor1 = "^Blazorise|^Syncfusion|^BouncyCastle|^EmbeddedBlazorContent|^Google|^IdentityServer4|^MailKit|^MimeKit|^MiniProfiler|^Mono|^MySql|^Renci|^Serilog|";
        private const string SkipBlazor2 = "^SshNet|^Syncfusion|^CSRedisCore|^EPPlus|^iTextSharp|^Polly|^SafeObjectPool|^EasyCaching|";
        private const string SkipAssemblies = SkipBlazor1 + SkipBlazor2 + "^System|^Mscorlib|^msvcr120|^Netstandard|^Microsoft|^Autofac|^Antlr3|^Iesi|^JetBrains|^Remotion|^NHibernate|^FluentNHibernate|^DevExpress|^AutoMapper|^EntityOrion.Framework|^Newtonsoft|^Castle|^NLog|^Pomelo|^AspectCore|^Xunit|^Nito|^Npgsql|^Exceptionless|^MySqlConnector|^Anonymously Hosted|^libuv|^api-ms|^clrcompression|^clretwrc|^clrjit|^coreclr|^dbgshim|^e_sqlite3|^hostfxr|^hostpolicy|^MessagePack|^mscordaccore|^mscordbi|^mscorrc|^sni|sos|SOS.NETCore|^sos_amd64|^SQLitePCLRaw|^StackExchange|^Swashbuckle|WindowsBase|ucrtbase|^DotNetCore.CAP|^MongoDB|^Confluent.Kafka|^librdkafka|^EasyCaching|^RabbitMQ|^Consul|^Dapper|^EnyimMemcachedCore|^Pipelines|^DnsClient|^IdentityModel|^zlib|^Accessibility|^SQLite";

        public virtual List<Assembly> GetAssemblies()
        {

            LoadAssemblies( PlatformServices.Default.Application.ApplicationBasePath );
            return GetAssembliesFromCurrentAppDomain();
        }

      
        protected void LoadAssemblies( string path ) 
        {
            //using (StreamWriter writer = new StreamWriter("c:\\usr\\assemblies.txt"))
            //{
                foreach (string file in Directory.GetFiles(path, "*.dll"))
                {
                    if (Match(Path.GetFileName(file)) == false)
                        continue;
                    //writer.WriteLine (file);
                    LoadAssemblyToAppDomain(file);
                }
            //}
        }

       
        protected virtual bool Match( string assemblyName ) {
            if( assemblyName.StartsWith( $"{PlatformServices.Default.Application.ApplicationName}.Views" ) )
                return false;
            if( assemblyName.StartsWith( $"{PlatformServices.Default.Application.ApplicationName}.PrecompiledViews" ) )
                return false;
            return Regex.IsMatch( assemblyName, SkipAssemblies, RegexOptions.IgnoreCase | RegexOptions.Compiled ) == false;
        }

        private void LoadAssemblyToAppDomain( string file ) {
            try {
                var assemblyName = AssemblyName.GetAssemblyName( file );
                AppDomain.CurrentDomain.Load( assemblyName );
            }
            catch ( BadImageFormatException ex )
            {
                Trace.WriteLine($" Erro ao carregar o {file}->{ex.Message}");
            }
        }

    
        private List<Assembly> GetAssembliesFromCurrentAppDomain() {
            var result = new List<Assembly>();
            foreach( var assembly in AppDomain.CurrentDomain.GetAssemblies() ) {
                if( Match( assembly ) )
                    result.Add( assembly );
            }
            return result.Distinct().ToList();
        }

        
        private bool Match( Assembly assembly ) {
            return !Regex.IsMatch( assembly.FullName, SkipAssemblies, RegexOptions.IgnoreCase | RegexOptions.Compiled );
        }

       
        public List<Type> Find<T>( List<Assembly> assemblies = null ) {
            return Find( typeof( T ), assemblies );
        }

       
        public List<Type> Find( Type findType, List<Assembly> assemblies = null ) {
            assemblies = assemblies ?? GetAssemblies();
            return Reflection.FindTypes( findType, assemblies.ToArray() );
        }
    }
}
