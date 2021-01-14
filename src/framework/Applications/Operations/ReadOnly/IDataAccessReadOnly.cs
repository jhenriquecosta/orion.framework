using System.Collections.Generic;
using System.Threading.Tasks;


namespace Orion.Framework.Infrastructurelications.Operations
{

    public interface IDataAccessReadOnly<TDto> where TDto : new() {

        //FindAll
        Task<List<TDto>> FindAllAsync();


    }
}
