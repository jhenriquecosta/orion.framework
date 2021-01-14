using System.Threading.Tasks;
using Orion.Framework.Applications.Dtos;
using Orion.Framework.Validations.Aspects;

namespace Orion.Framework.Web.Applications.Operations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface ISaveAsync<in TRequest> where TRequest : IRequest, IKey, new() {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        //[UnitOfWork]
        Task SaveAsync( [Valid] TRequest request );
    }
}