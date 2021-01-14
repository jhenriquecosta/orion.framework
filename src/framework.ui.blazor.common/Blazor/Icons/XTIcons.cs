using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Web.Blazor.Components.Icons
{
    public static class XTIcons
    {
        public static List<DataItem> GetFeather()
        {
           return Framework.Web.Helpers.Enum.GetItems<XTSimpleLineIcon>();
        }
        public static List<DataItem> GetFontAwesome()
        {
            return Framework.Web.Helpers.Enum.GetItems<XTFontAwesomeIcon>();
        }
    }
}
