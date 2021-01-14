using System.Threading.Tasks;
using Orion.Framework.Validations.Aspects;
using Orion.Framework.Applications.Dtos;

namespace Orion.Framework.Applications.Operations
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