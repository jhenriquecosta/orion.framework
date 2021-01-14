using System.Runtime.Serialization;
using Orion.Framework.Security.Common;

namespace Orion.Framework.Security.Domain.Dtos
{
    [DataContract]
    public class ApiResponseDto<T>
    {
        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        public bool IsSuccessStatusCode => StatusCode / 200 == 1;

        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ResponseException { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PaginationDetails PaginationDetails { get; set; } = null;

        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }
    }

    [DataContract]
    public class ApiResponseDto : ApiResponseDto<object>
    { }
}
