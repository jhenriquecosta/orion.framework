using Orion.Framework.Applications.Dtos;
using Orion.Framework.Validations.Aspects;

namespace Orion.Framework.Web.Applications.Operations
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