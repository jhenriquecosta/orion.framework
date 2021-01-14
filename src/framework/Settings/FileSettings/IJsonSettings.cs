using System;
using System.Collections.Generic;
using System.Text;
using Orion.Framework.Dependency;

namespace Orion.Framework.Applications
{
    public interface IJsonSettings: ISingletonDependency
    {
       string GetValue(string section, string value);

    }
}
