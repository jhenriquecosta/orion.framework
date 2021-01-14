using Newtonsoft.Json;

namespace Orion.Framework.Json {
    /// <summary>
    /// 
    /// </summary>
    public static class Json {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public static T ToObject<T>( string json ) {
            if ( string.IsNullOrWhiteSpace( json ) )
                return default(T);
            return JsonConvert.DeserializeObject<T>( json );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isConvertToSingleQuotes"></param>
        public static string ToJson( object target,bool isConvertToSingleQuotes = false ) 
        {
            if (target == null)
                return string.Empty;
            //var result = JsonConvert.SerializeObject(target);

            var result = JsonConvert.SerializeObject(target, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });



            if (isConvertToSingleQuotes)
                result = result.Replace("\"", "'");
            return result;

            //if ( target == null )
            //    return "{}";
            //var result = JsonConvert.SerializeObject( target );
            ////var result = JsonConvert.SerializeObject(target, new JsonSerializerSettings()
            ////{
            ////    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ////    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ////    Formatting = Formatting.Indented
            ////});
            //if ( isConvertToSingleQuotes )
            //    result = result.Replace( "\"", "'" );
            //return result;
        }
    }
}
//public static class ContactSerialize
//{
//    public static string ToJson(this Contact self) => JsonConvert.SerializeObject(self, Converter.Settings);
//}

//public class Converter
//{
//    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
//    {
//        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//        DateParseHandling = DateParseHandling.None,
//        Formatting = Formatting.Indented,
//        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
//        ContractResolver = new CamelCasePropertyNamesContractResolver()
//    };
//}