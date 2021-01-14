using System;
using System.Runtime.Serialization;
using Orion.Framework.Caches;

namespace Orion.Framework.Domains.ValueObjects
{
    [Serializable]
    [DataContract]
    public class AppDataTransfer
    {
        public string Source { get; set; }
        [DataMember]
      
        public bool IsError { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public object Result { get; set; }       
    }
}
