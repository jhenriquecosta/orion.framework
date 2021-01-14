using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Extensions
{
    /// <summary>
    /// Provides helper methods for the Assembly class
    /// </summary>
    internal static class AssemblyExtensions
    {
        /// <summary>
        /// Tries to load an assembly from a given file
        /// </summary>
        /// <param name="assemblyFile">The file full path</param>
        /// <returns>The assembly</returns>
        public static Assembly TryLoadFrom(string assemblyFile)
        {
            Assembly result = null;
            try
            {
                result = Assembly.LoadFrom(assemblyFile);
            }
            catch 
            { 
            }

            return result;
        }
    }
}
