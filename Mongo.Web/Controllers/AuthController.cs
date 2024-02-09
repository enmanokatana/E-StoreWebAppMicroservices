using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mongo.Web.Models;
using Mongo.Web.Service.IService;
using Mongo.Web.Utility;

namespace Mongo.Web.Controllers;

public class AuthController :Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
        
        return Ok();
    } 
}