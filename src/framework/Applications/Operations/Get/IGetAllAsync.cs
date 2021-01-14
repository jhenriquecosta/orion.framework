using System.Collections.Generic;
using System.Threading.Tasks;


namespace Orion.Framework.Infrastructurelications.Operations
{

    public interface IGetAllAsync<TDto> where TDto : new()
    {
        Task<List<TDto>> GetAllAsync();
    }
}
