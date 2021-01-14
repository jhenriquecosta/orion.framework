using System.ComponentModel;

namespace Orion.Framework.Web.Clients {

    public enum HttpContentType {
      
        [Description( "application/x-www-form-urlencoded" )]
        FormUrlEncoded,
       
        [Description( "application/json" )]
        Json,
       
        [Description( "text/xml" )]
        Xml
    }
}
