using Mongo.Services.AuthAPI.Models.Dto;

namespace Mongo.Services.AuthAPI.Service.IService;

public interface IAuthService
{
    Task<string> Register(RegiterationRequestDto regiterationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignedRole(string Email, string rolename);
}