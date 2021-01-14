using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Orion.Framework.Domains.ValueObjects
{
    public class EditValue 
    {  

        [JsonProperty( "value", NullValueHandling = NullValueHandling.Ignore )]
        public object Value { get; set;  }
        public Type ValueType { get; set; }
        public List<DataItem> ValueData { get; set; }

    }
}
