using Mongo.Web.Models;

namespace Mongo.Web.Service.IService;

public interface IAuthService
{
    Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
    Task<ResponseDto?> RegisterAsync(RegiterationRequestDto regiterationRequestDto);
    Task<ResponseDto?> AssignRoleAsync(RegiterationRequestDto regiterationRequestDto);
    
}