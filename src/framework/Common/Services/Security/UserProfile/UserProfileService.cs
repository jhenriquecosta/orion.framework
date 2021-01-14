using AutoMapper;
using Gestor.Settings.Domain.Dtos;
using Gestor.Settings.Domain.Entities;
using Orion.Framework.App.Services.Domain.Dtos;
using Orion.Framework.App.Services.Domain.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeus.Domains.Services;
using Zeus.Utility;
using Zeus.Webs.Api;

namespace Orion.Framework.App.Services.Services
{
    public interface IUserProfileService
    {
        Task<ApiResponse> Get(string userId);
        Task<ApiResponse> Upsert(UserProfileDto userProfile);
        string GetLastPageVisited(string userName);
    }
    public class UserProfileService : IUserProfileService
    {
        private readonly ISession _session;
        private readonly IMapper _autoMapper;
        private readonly DbContextGenericDao<UserProfile> _dao = new DbContextGenericDao<UserProfile>();
       

        public string GetLastPageVisited(string userName)
        {
         
            var userProfile =AsyncUtil.RunSync( ()=> _dao.FindAllAsync(f => f.User.UserName.Equals(userName))); 
            string lastPageVisited = "/dashboard";

            //var userProfile = from userProf in _db.UserProfiles
            //                  join user in _db.Users on userProf.UserId equals user.Id
            //                  where user.UserName == userName
            //                  select userProf;

            if (userProfile.Any())
            {
                lastPageVisited = !String.IsNullOrEmpty(userProfile.First().LastPageVisited) ? userProfile.First().LastPageVisited : lastPageVisited;
            }

            return lastPageVisited;
        }

        public async Task<ApiResponse> Get(string userId)
        {
            var profileQuery = await _dao.FindAllAsync(f=>f.User.Id.Equals(userId));
            //var profileQuery = from userProf in _userProfiles
            //                   where userProf.UserId == userId
            //                   select userProf;

            UserProfileDto userProfile = new UserProfileDto();
            if (!profileQuery.Any())
            {
                userProfile = new UserProfileDto
                {
                    UserId = userId
                };
            }
            else
            {
                UserProfile profile = profileQuery.First();
                userProfile.Count = profile.Count;
                userProfile.IsNavOpen = profile.IsNavOpen;
                userProfile.LastPageVisited = profile.LastPageVisited;
                userProfile.IsNavMinified = profile.IsNavMinified;
                userProfile.UserId = userId;
            }

            return new ApiResponse(200, "Retrieved User Profile", userProfile);
        }

        public async Task<ApiResponse> Upsert(UserProfileDto userProfileDto)
        {
            try
            {
                var profileQuery = AsyncUtil.RunSync(() => _dao.FindAllAsync(f=>f.User.Id.Equals(userProfileDto.Id)));
                var _daoUser = new DbContextGenericDao<ApplicationUser>();
                var _user = await _daoUser.LoadAsync(userProfileDto.Id);
                if (profileQuery.Any())
                {
                   
                    UserProfile profile = profileQuery.First();
                    //_autoMapper.Map<UserProfileDto, UserProfile>(userProfileDto, profile);
                    profile.User = _user;
                    profile.Count = userProfileDto.Count;
                    profile.IsNavOpen = userProfileDto.IsNavOpen;
                    profile.LastPageVisited = userProfileDto.LastPageVisited;
                    profile.IsNavMinified = userProfileDto.IsNavMinified;
                    profile.LastUpdatedDate = DateTime.Now;
                    AsyncUtil.RunSync(() =>_dao.SaveOrUpdateAsync(profile));
                    AsyncUtil.RunSync(() => _dao.FlushChangesAsync());
                    
                }
                else
                {
                    //TODO review automapper settings
                    //_autoMapper.Map<UserProfileDto, UserProfile>(userProfileDto, profile);

                    UserProfile profile = new UserProfile
                    {
                        User = _user,
                        Count = userProfileDto.Count,
                        IsNavOpen = userProfileDto.IsNavOpen,
                        LastPageVisited = userProfileDto.LastPageVisited,
                        IsNavMinified = userProfileDto.IsNavMinified,
                        LastUpdatedDate = DateTime.Now
                    };
                    AsyncUtil.RunSync(() => _dao.SaveOrUpdateAsync(profile));
                    AsyncUtil.RunSync(() => _dao.FlushChangesAsync());
                }

                return new ApiResponse(200, "Updated User Profile");
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                return new ApiResponse(400, "Failed to Retrieve User Profile");
            }
        }
    }
}
