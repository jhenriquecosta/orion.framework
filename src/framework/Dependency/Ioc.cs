using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework.Dependency;
using Orion.Framework.Domains.ValueObjects;

namespace Orion.Framework
{

    public static class Ioc
    {
    
        internal static readonly Container DefaultContainer = new Container();
        internal static readonly List<DataItem> ManagerServices = new List<DataItem>();
       
        public static IContainer CreateContainer( params IConfig[] configs ) {
            var container = new Container();
            container.Register( null, builder => builder.EnableAop(), configs );
            return container;
        }

        public static Autofac.IContainer GetContainer()
        {
            return DefaultContainer.GetContainer();
        }
        public static List<T> CreateList<T>( string name = null ) {
            return DefaultContainer.CreateList<T>( name );
        }

       
        public static List<T> CreateList<T>( Type type, string name = null ) {
            return ( (IEnumerable<T>)DefaultContainer.CreateList( type, name ) ).ToList();
        }

        public static dynamic Create(string name)
        {
           return DefaultContainer.Create(name);           
        }
        public static dynamic Create(Type type)
        {
            return DefaultContainer.Create(type);
        }
        public static T Create<T>( string name = null )
        {
            return DefaultContainer.Create<T>( name );
        }

     
        public static T Create<T>( Type type, string name = null )
        {
            return (T)DefaultContainer.Create( type, name );
        }

    
        public static IScope BeginScope() 
        {
            return DefaultContainer.BeginScope();
        }

        
        public static void Register( params IConfig[] configs ) {
            DefaultContainer.Register( null, builder => builder.EnableAop(), configs );
        }

      
        public static IServiceProvider Register( IServiceCollection services, params IConfig[] configs ) {
            return DefaultContainer.Register( services, builder => builder.EnableAop(), configs );
        }

        public static void AddServices(Guid? id,string named, object services,Type serviceType)
        {
            var data = ManagerServices.SingleOrDefault(f => f.Id.Equals(id));
            if (data != null)
            {
                ManagerServices.Remove(data);
            }
            data = new DataItem {Id = id, Text = named, Value = services,ValueType = serviceType};
            ManagerServices.Add(data);
        }
        public static T GetServices<T>(Guid? id, string named, object services)
        {
            var data = ManagerServices.SingleOrDefault(f => f.Id.Equals(id));
            if (data == null)
            {
                return default(T);
            }
            return (T) data.Value;
        }


        public static void Dispose() 
        {
            DefaultContainer.Dispose();
        }
    }
}
