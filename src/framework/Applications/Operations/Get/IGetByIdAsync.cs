using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Framework.Infrastructurelications.Operations
{
  
 
    public interface IGetByIdAsync<TDto> where TDto : new() {
    
        Task<TDto> GetByIdAsync( object id );
       
        Task<List<TDto>> GetByIdsAsync( string ids );
    }
}