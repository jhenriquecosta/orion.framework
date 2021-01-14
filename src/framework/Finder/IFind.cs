using System;
using System.Collections.Generic;
using System.Reflection;

namespace Orion.Framework.Reflections {
   
    public interface IFind {
      
        List<Assembly> GetAssemblies();
       
        List<Type> Find<T>( List<Assembly> assemblies = null );
      
        List<Type> Find( Type findType, List<Assembly> assemblies = null );
    }
}
