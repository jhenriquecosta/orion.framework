using Orion.Framework.Validations.Aspects;
using Orion.Framework.Applications.Dtos;

namespace Orion.Framework.Applications.Operations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCreateRequest"></typeparam>
    public interface ICreate<in TCreateRequest> where TCreateRequest : IRequest, new() {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        //[UnitOfWork]
        string Create( [Valid] TCreateRequest request );
    }
}