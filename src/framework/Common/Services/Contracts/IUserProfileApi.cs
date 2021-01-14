using Orion.Framework.Common.Domain.Dtos;
using System.Threading.Tasks;

namespace Orion.Framework.Common.Services.Contracts
{
    /// <summary>
    /// Access to User Profile information
    /// </summary>
    public interface IUserProfileApi
    {
        Task<ApiResponseDto> Upsert(UserProfileDto userProfile);
        Task<ApiResponseDto> Get();
    }
}
