using System.Threading.Tasks;
using Orion.Framework.Applications.Dtos;
using Orion.Framework.Validations.Aspects;

namespace Orion.Framework.Applications.Services.Contracts
{
    public interface ICrudServiceBase 
    {
       
        Task SaveOrUpdateAsync([Valid] IRequest item);
       

    }
}
