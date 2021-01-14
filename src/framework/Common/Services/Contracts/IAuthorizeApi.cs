﻿using Orion.Framework.Common.Domain.Dtos;
using System.Threading.Tasks;

namespace Orion.Framework.Common.Services.Contracts
{
    public interface IAuthorizeApi
    {
        Task<ApiResponseDto> Login(LoginDto loginParameters);
        Task<ApiResponseDto> Create(RegisterDto registerParameters);
        Task<ApiResponseDto> Register(RegisterDto registerParameters);
        Task<ApiResponseDto> ForgotPassword(ForgotPasswordDto forgotPasswordParameters);
        Task<ApiResponseDto> ResetPassword(ResetPasswordDto resetPasswordParameters);
        Task<ApiResponseDto> Logout();
        Task<ApiResponseDto> ConfirmEmail(ConfirmEmailDto confirmEmailParameters);
        Task<UserInfoDto> GetUserInfo();
        Task<ApiResponseDto> UpdateUser(UserInfoDto userInfo);
        Task<UserInfoDto> GetUser();
    }
}
