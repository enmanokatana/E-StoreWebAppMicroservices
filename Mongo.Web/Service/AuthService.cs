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
            ApiType = SD.ApiType.GET,
            Data =loginRequestDto, 
            Url = SD.CouponApiBase + "/api/auth/Login"

        });
    }

    public async Task<ResponseDto?> RegisterAsync(RegiterationRequestDto regiterationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Data = regiterationRequestDto, 
            Url = SD.CouponApiBase + "/api/auth/register"
            
        });    }

    public async Task<ResponseDto?> AssignRoleAsync(RegiterationRequestDto regiterationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Data = regiterationRequestDto, 
            Url = SD.CouponApiBase + "/api/auth/AssignRole"
            
        });
        
    }
    
    }