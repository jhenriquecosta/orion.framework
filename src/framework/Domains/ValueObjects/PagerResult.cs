using Orion.Framework.Applications.Dtos;
using Orion.Framework.Domains.Repositories;

namespace Orion.Framework.Domains.ValueObjects
{
    public class PagerResult<TDto> : IResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public PagerList<TDto> Data { get; set; } = new PagerList<TDto>();
    }
}
