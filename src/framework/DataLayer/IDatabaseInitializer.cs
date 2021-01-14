using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orion.Framework.Dependency;

namespace Orion.Framework.DataLayer
{
    public interface IDatabaseInitializer : IScopeDependency
    {
       
        Task SeedAsync();
    }
}
