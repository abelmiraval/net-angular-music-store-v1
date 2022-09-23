using MusicStore.Dto.Request;
using MusicStore.Dto.Response;

namespace MusicStore.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponseGeneric<string>> RegisterAsync(DtoRegisterUser request);

    Task<DtoLoginResponse> LoginAsync(DtoLogin request);
    Task<BaseResponseGeneric<string>> SendTokenToResetPasswordAsync(DtoResetPassword request);
    Task<BaseResponseGeneric<string>> ResetPassword(DtoConfirmReset request);
}