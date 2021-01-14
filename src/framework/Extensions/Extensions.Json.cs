using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Orion.Framework.Json;

namespace Orion.Framework 
{
    public static partial class Extensions
    {

        public static string ToJson(this object obj) => Json.Json.ToJson(obj);
        public static T JsonToObject<T>(this string obj) => Json.Json.ToObject<T>(obj);

        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            options.Converters.Insert(0, new DateTimeConverter());

            return JsonConvert.SerializeObject(obj, options);
        }
        public static bool IsValidJson(this string text)
        {
            text = text.Trim();
            if ((text.StartsWith("{") && text.EndsWith("}")) || //For object
                (text.StartsWith("[") && text.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(text);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
