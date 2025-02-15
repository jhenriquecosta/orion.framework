﻿using Orion.Framework.App.Services.Domain.Dtos;
using Orion.Framework.App.Services.Domain.ValueObjects;
using Orion.Framework.App.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Zeus.Webs.Api;

namespace Orion.Framework.App.Services.Services
{
    public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthorizeApi _authorizeApi;
        private readonly IAccountManager _accountManager;
        private readonly AppState _appState;

        public IdentityAuthenticationStateProvider(IAccountManager accountManager, IAuthorizeApi authorizeApi, AppState appState)
        {
            _authorizeApi = authorizeApi;
            _accountManager = accountManager;
            _appState = appState;
        }

        public async Task<ApiResponseDto> Login(LoginDto loginParameters)
        {
            ApiResponseDto apiResponse = await _authorizeApi.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return apiResponse;
        }

        public async Task<ApiResponseDto> Register(RegisterDto registerParameters)
        {
            ApiResponseDto apiResponse = await _authorizeApi.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return apiResponse;
        }

        public async Task<ApiResponseDto> Create(RegisterDto registerParameters)
        {
            ApiResponseDto apiResponse = await _authorizeApi.Create(registerParameters);
            return apiResponse;
        }

        public async Task<ApiResponseDto> Logout()
        {
            _appState.UserProfile = null;
            ApiResponseDto apiResponse = await _authorizeApi.Logout();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return apiResponse;
        }

        public async Task<ApiResponseDto> ConfirmEmail(ConfirmEmailDto confirmEmailParameters)
        {
            ApiResponseDto apiResponse = await _authorizeApi.ConfirmEmail(confirmEmailParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return apiResponse;
        }

        public async Task<ApiResponseDto> ResetPassword(ResetPasswordDto resetPasswordParameters)
        {
            ApiResponseDto apiResponse = await _authorizeApi.ResetPassword(resetPasswordParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return apiResponse;
        }

        public async Task<ApiResponseDto> ForgotPassword(ForgotPasswordDto forgotPasswordParameters)
        {
            ApiResponseDto apiResponse = await _authorizeApi.ForgotPassword(forgotPasswordParameters);
            return apiResponse;
        }

        public async Task<UserInfoDto> GetUserInfo()
        {
            UserInfoDto userInfo =  _accountManager.GetUser();
            bool IsAuthenticated = userInfo.IsAuthenticated;
            if (IsAuthenticated)
            {
                userInfo = await _authorizeApi.GetUserInfo();
            }
            else
            {
                userInfo = new UserInfoDto { IsAuthenticated = false, Roles = new List<string>() };
            }
            return userInfo;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetUserInfo();
                if (userInfo.IsAuthenticated)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, userInfo.UserName) }.Concat(userInfo.ExposedClaims.Select(c => new Claim(c.Key, c.Value)));
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task<ApiResponseDto> UpdateUser(UserInfoDto userInfo)
        {
            ApiResponseDto apiResponse = await _authorizeApi.UpdateUser(userInfo);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return apiResponse;
        }
    }
}