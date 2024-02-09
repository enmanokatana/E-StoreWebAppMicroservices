using Mongo.Web.Models;
using Mongo.Web.Service.IService;
using Mongo.Web.Utility;

namespace Mongo.Web.Service;

public class AuthService:IAuthService
{
    private readonly IBaseService _baseService;
    
    
    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data =loginRequestDto, 
            Url = SD.AuthApiBase + "/api/auth/Login"

        });
    }

    public async Task<ResponseDto?> RegisterAsync(RegiterationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = registrationRequestDto,
            Url = SD.AuthApiBase + "/api/auth/register"
        
    });
    }
    

    public async Task<ResponseDto?> AssignRoleAsync(RegiterationRequestDto regiterationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = regiterationRequestDto, 
            Url = SD.AuthApiBase + "/api/auth/AssignRole"
            
        });
        
    }
    
    }