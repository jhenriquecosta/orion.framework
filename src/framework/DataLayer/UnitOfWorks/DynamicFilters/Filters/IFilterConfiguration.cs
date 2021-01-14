using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.DataLayer.Filters
{
    public interface IFilterConfiguration
    {
        string Name();
        IDictionary<string, object> Parameters();
    }
}
