using System.Threading.Tasks;
using Orion.Framework.Applications.Dtos;
using Orion.Framework.Validations.Aspects;


namespace Orion.Framework.Applications.Operations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCreateRequest"></typeparam>
    public interface ICreateAsync<in TCreateRequest> where TCreateRequest : IRequest, new() {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        //[UnitOfWork]
        Task<string> CreateAsync( [Valid] TCreateRequest request );
    }
}