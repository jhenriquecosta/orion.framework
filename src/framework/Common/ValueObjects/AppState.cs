using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Orion.Framework.Common.Domain.Dtos;

namespace Orion.Framework.Common.ValueObjects
{
    public class AppState
    {
        public event Action OnChange;
        private readonly IUserProfileApi _userProfileApi;

        public List<OXMenuItem> MenuItems { get; set; }
        public UserProfileDto UserProfile { get; set; }

        //public AppState(IUserProfileApi userProfileApi)
        //{
        //    _userProfileApi = userProfileApi;
        //}

        public bool IsNavOpen
        {
            get
            {
                if (UserProfile == null)
                {
                    return true;
                }
                return UserProfile.IsNavOpen;
            }
            set
            {
                UserProfile.IsNavOpen = value;
            }
        }
        public bool IsNavMinified { get; set; }

        public async Task UpdateUserProfile()
        {
            await _userProfileApi.Upsert(UserProfile);
        }

        public async Task<UserProfileDto> GetUserProfile()
        {
            if (UserProfile != null && UserProfile.UserId != string.Empty)
            {
                return UserProfile;
            }

            ApiResponseDto apiResponse = await _userProfileApi.Get();

            if (apiResponse.StatusCode == 200)
            {
                return JsonConvert.DeserializeObject<UserProfileDto>(apiResponse.Result.ToString());
            }
            return new UserProfileDto();
        }
      
        public async Task UpdateUserProfileCount(int count)
        {
            UserProfile.Count = count;
            await UpdateUserProfile();
            NotifyStateChanged();
        }

        public async Task<int> GetUserProfileCount()
        {
            if (UserProfile == null)
            {
                UserProfile = await GetUserProfile();
                return UserProfile.Count;
            }

            return UserProfile.Count;
        }

        public async Task SaveLastVisitedUri(string uri)
        {
            if (UserProfile ==  null)
            {
                UserProfile = await GetUserProfile();
            }
            if (UserProfile != null)
            {
                UserProfile.LastPageVisited = uri;
                await UpdateUserProfile();
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
        public void Update(NotifyProperties prop)
        {
            if (Notify != null)
            {
                this.Prop = prop;
                Notify.Invoke(prop);
            }
        }
        public NotifyProperties Prop
        {
            get; set;
        } = new NotifyProperties();

        public event Func<NotifyProperties, Task> Notify;
    }


    public class NotifyProperties
    {
        public bool HideSpinner { get; set; }

        public bool RestricMouseEvents { get; set; }
    }
}
