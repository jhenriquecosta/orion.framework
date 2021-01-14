using System;
using System.Collections.Generic;
using System.Text;
using Orion.Framework.Settings;

namespace Orion.Framework.Core
{
    public class Headers : Dictionary<string, object>
    {
        public string GetCorrelationId()
        {
            return ContainsKey(XTConstants.Events.CorrelationId)
                ? this[XTConstants.Events.CorrelationId].ToString()
                : string.Empty;
        }

        public string GetCausationId()
        {
            return ContainsKey(XTConstants.Events.CausationId)
                ? this[XTConstants.Events.CausationId].ToString()
                : string.Empty;
        }

        public string GetUserId()
        {
            return ContainsKey(XTConstants.Events.UserId)
                ? this[XTConstants.Events.UserId].ToString()
                : string.Empty;
        }

        public string GetSourceType()
        {
            return ContainsKey(XTConstants.Events.SourceType)
                ? this[XTConstants.Events.SourceType].ToString()
                : string.Empty;
        }

        public string GetQualifiedName()
        {
            return ContainsKey(XTConstants.Events.QualifiedName)
                ? this[XTConstants.Events.QualifiedName].ToString()
                : string.Empty;
        }

        public string GetAggregateId()
        {
            return ContainsKey(XTConstants.Events.AggregateId)
                ? this[XTConstants.Events.AggregateId].ToString()
                : string.Empty;
        }

        public string Get(string key)
        {
            return ContainsKey(key)
                ? this[key].ToString()
                : string.Empty;
        }

        public override string ToString()
        {
            return this.ToJsonString(true, true);
        }
    }
}
