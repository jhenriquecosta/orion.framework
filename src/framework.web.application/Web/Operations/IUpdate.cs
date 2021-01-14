using Orion.Framework.Validations.Aspects;
using Orion.Framework.Applications.Dtos;

namespace Orion.Framework.Applications.Operations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUpdateRequest"></typeparam>
    public interface IUpdate<in TUpdateRequest> where TUpdateRequest : IRequest, new() {
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        //[UnitOfWork]
        void Update( [Valid] TUpdateRequest request );
    }
}