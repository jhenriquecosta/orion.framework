using Orion.Framework.App.Services.Domain.Dtos;
using Orion.Framework.App.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using Zeus.Webs.Api;

namespace Orion.Framework.App.Services.Services
{
    public class UserProfileApi : IUserProfileApi
    {
        private readonly HttpClient _httpClient;

        public UserProfileApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponseDto> Get()
        {
            return await _httpClient.GetJsonAsync<ApiResponseDto>("api/UserProfile/Get");
        }

        public async Task<ApiResponseDto> Upsert(UserProfileDto userProfile)
        {
            return await _httpClient.PostJsonAsync<ApiResponseDto>("api/UserProfile/Upsert", userProfile);
        }
    }
}
