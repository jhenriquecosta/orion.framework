using System.Threading.Tasks;
using Orion.Framework.Applications.Dtos;
using Orion.Framework.Validations.Aspects;

namespace Orion.Framework.Web.Applications.Operations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUpdateRequest"></typeparam>
    public interface IUpdateAsync<in TUpdateRequest> where TUpdateRequest : IRequest, new() {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        //[UnitOfWork]
        Task UpdateAsync( [Valid] TUpdateRequest request );
    }
}