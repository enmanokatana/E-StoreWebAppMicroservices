﻿using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mongo.Web.Models;
using Mongo.Web.Service.IService;
using Mongo.Web.Utility;
using Newtonsoft.Json;

namespace Mongo.Web.Controllers;

public class AuthController :Controller
{
    private readonly IAuthService _authService;
    private readonly ITokenProvider _tokenProvider;

    public AuthController(IAuthService authService, ITokenProvider tokenProvider)
    {
        _authService = authService;
        _tokenProvider = tokenProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        LoginRequestDto loginRequestDto = new LoginRequestDto();
        return View(loginRequestDto);
    }
    [HttpGet]
    public IActionResult Register()
    {
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
            new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
        };

        ViewBag.RoleList = roleList;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto obj)
    {
        ResponseDto? responseDto = await _authService.LoginAsync(obj);

        if(responseDto!=null && responseDto.IsSuccess)
        {
            LoginResponseDto loginResponseDto =
                JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString((responseDto.Result)));
            await SignInUser(loginResponseDto);
            _tokenProvider.setToken(loginResponseDto.Token);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError("CustomError", responseDto.Message);
            return View(obj);
        }

        var roleList = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
            new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
        };

        ViewBag.RoleList = roleList;
        return View(obj);
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegiterationRequestDto obj)
    {
        ResponseDto? result = await _authService.RegisterAsync(obj);
        ResponseDto? assingRole;

        if(result!=null && result.IsSuccess)
        {
            if (string.IsNullOrEmpty(obj.Rolename))
            {
                obj.Rolename = SD.RoleCustomer;
            }
            assingRole = await _authService.AssignRoleAsync(obj);
            if (assingRole!=null && assingRole.IsSuccess)
            {
                TempData["success"] = "Registration Successful";
                return RedirectToAction(nameof(Login));
            }
        }
        else
        {
            TempData["error"] = result.Message;
        }

        var roleList = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
            new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
        };

        ViewBag.RoleList = roleList;
        return View(obj);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();
        
        return RedirectToAction("Index","Home");
    }

    private async Task SignInUser(LoginResponseDto model)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(model.Token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(        new Claim(JwtRegisteredClaimNames.Email, 
                jwt.Claims.FirstOrDefault(u=>u.Type==JwtRegisteredClaimNames.Email).Value));
      
        identity.AddClaim(        new Claim(JwtRegisteredClaimNames.Sub, 
            jwt.Claims.FirstOrDefault(u=>u.Type==JwtRegisteredClaimNames.Sub).Value));
        
        identity.AddClaim(        new Claim(JwtRegisteredClaimNames.Name, 
            jwt.Claims.FirstOrDefault(u=>u.Type==JwtRegisteredClaimNames.Name).Value));
        
        identity.AddClaim(        new Claim(ClaimTypes.Name, 
            jwt.Claims.FirstOrDefault(u=>u.Type==JwtRegisteredClaimNames.Email).Value));


        identity.AddClaim(new Claim(ClaimTypes.Role, 
            jwt.Claims.FirstOrDefault(u=>u.Type=="role").Value));






        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}