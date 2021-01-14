using Orion.Framework.App.Services.Domain.Dtos;
using Orion.Framework.App.Services.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zeus.Domains.Services;
using Zeus.Maps;
using Zeus.Security.Sessions;
using Zeus.Webs.Api;

namespace Orion.Framework.App.Services.Services
{

    public interface IApiLogService
    {
        Task Log(ApiLogItem apiLogItem);
        Task<ApiResponse> Get();
        Task<ApiResponse> GetByApplictionUserId(string applicationUserId);
    }
    public class ApiLogService : IApiLogService
    {
      
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSession _userSession;
        private readonly DbContextGenericDao<ApiLogItem> _dao = new DbContextGenericDao<ApiLogItem>();

        public ApiLogService(IConfiguration configuration,  UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IUserSession userSession)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userSession = userSession;

        }

        public async Task Log(ApiLogItem apiLogItem)
        {
            if (apiLogItem.User != null)
            {
                //TODO populate _userSession??

                //var currentUser = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                //UserSession userSession = new UserSession();
                //if (currentUser != null)
                //{
                //    userSession = new UserSession(currentUser.Result);
                //}
            }
            else
            {
                apiLogItem.User = null;
            }
            await _dao.SaveOrUpdateAsync(apiLogItem);
            await _dao.FlushChangesAsync();

        }

        public async Task<ApiResponse> Get()
        {
            var _apiLog =await _dao.FindAllAsync();
            var _result = _apiLog.MapTo<List<ApiLogItemDto>>();
            return new ApiResponse(200, "Retrieved Api Log",_result);
        }

        public async Task<ApiResponse> GetByApplictionUserId(string applicationUserId)
        {
            try
            {
                var _apiLog = await _dao.FindAllAsync(f=>f.User.Id.Equals(applicationUserId));
                var _result = _apiLog.MapTo<List<ApiLogItemDto>>();
                return new ApiResponse(200, "Retrieved Api Log",_result);
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }
    }
}
