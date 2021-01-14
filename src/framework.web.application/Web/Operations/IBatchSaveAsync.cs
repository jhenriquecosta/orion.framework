using Orion.Framework.DataLayer.Web.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Framework.Applications.Operations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public interface IBatchSaveAsync<TDto>
        where TDto : IDto, new() {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creationList"></param>
        /// <param name="updateList"></param>
        /// <param name="deleteList"></param>
        Task<List<TDto>> SaveAsync( List<TDto> creationList, List<TDto> updateList, List<TDto> deleteList );
    }
}