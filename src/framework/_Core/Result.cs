using System;
using System.Runtime.Serialization;
using Orion.Framework.Caches;

namespace Orion.Framework
{
    [Serializable]
    [DataContract]
    public class Result<T>
    {
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public bool IsOk { get; set; } = true;
        [DataMember]
        public bool IsError { get; set; } = false;
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public T Data { get; set; }
        public Result(T data,bool error=false) : this(data)
        {
            Data = data;
            IsError = error;
        }
        public Result(T data) : this()
        {
            Data = data;            
        }
        public Result()
        {
           
        }
    }
}
