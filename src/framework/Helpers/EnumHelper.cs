using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orion.Framework.Domains.ValueObjects;

namespace Orion.Framework.Helpers
{
    
    public class EnumHelper<TEnum>
    {
        private static List<DataItemCombo> internalData;
        public EnumHelper()
        {   
            internalData = Orion.Framework.Helpers.HelperEnum.GetItems<TEnum>().Select(x => new DataItemCombo { Key = x.Key, Text = x.Text, Value = typeof(TEnum) }).ToList();
        }
        public List<DataItemCombo> GetAll()
        {
       
            return internalData;
        }
        public object GetValue(object value)
        {
            var id = int.Parse(value.ToString());
            var icon = internalData.FirstOrDefault(f => f.Key.Equals(id));
            if (icon == null) icon = internalData.First();
            return icon.Key;
        }
        public object GetValueFromText(object value)
        { 
            var icon = internalData.FirstOrDefault(f => f.Text.Equals(value.ToString()));
            if (icon == null) icon = internalData.First();
            return icon.Key;
        }
    }
}
