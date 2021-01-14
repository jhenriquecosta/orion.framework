using System;
using Newtonsoft.Json;

namespace Orion.Framework.Domains.ValueObjects
{
    public class DataItem : IComparable<DataItem> 
    {

        public DataItem() { }    
        public DataItem(int id, object value)
        {
            Key = id;
            Value = value;
        }
        public DataItem(int id,object value, Type type) : this(id,value)
        {  
            ValueType = type;
        }
        public DataItem(int id, object value, Type type,string text) : this(id,value,type)
        { 
            Text = text;
        }
        //public DataItem(object id, string text,object value)
        //{
        //    Key = id;
        //    Text = text.ToString();
        //    Value = value;
        //}

        public DataItem(string text, object value, int? sortId = null, string group = null, bool? disabled = null) {

            Key = (int)value;
            Text = text;
            Value = value;
            SortId = sortId;
            Group = group;
            Disabled = disabled;
        }

        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }

        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public int Key { get; set; }
      
        [JsonProperty( "text", NullValueHandling = NullValueHandling.Ignore )]
        public string Text { get; set; }
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; set; }

        [JsonProperty( "valuetype", NullValueHandling = NullValueHandling.Ignore )]
        public Type ValueType { get; set; }

        [JsonProperty( "sortId", NullValueHandling = NullValueHandling.Ignore )]
        public int? SortId { get; }

        [JsonProperty( "group", NullValueHandling = NullValueHandling.Ignore )]
        public string Group { get; }

        [JsonProperty( "disabled", NullValueHandling = NullValueHandling.Ignore )]
        public bool? Disabled { get; }

        public int CompareTo( DataItem other ) {
            return string.Compare( Text, other.Text, StringComparison.CurrentCulture );
        }
    }
}
