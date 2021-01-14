using Orion.Framework.Settings;
using System.Collections.Generic;

namespace Orion.Framework.DataLayer.Filters
{

    public class SoftDeleteFilterConfiguration : IFilterConfiguration
    {
        public string Name()
        {
            return XTConstants.Filters.SoftDelete;
        }

        public IDictionary<string, object> Parameters()
        {
            var parameters = new Dictionary<string, object>
            {
                { XTConstants.Filters.SoftDeleteParamName, true }
            };
            return parameters;
        }
    }
}
