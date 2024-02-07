using Microsoft.AspNetCore.Mvc;
using Mongo.Web.Models;
using Mongo.Web.Service.IService;
using Newtonsoft.Json;

namespace Mongo.Web.Controllers;

public class CouponController : Controller
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }
    
    // GET
    public async Task<IActionResult> CouponIndex()
    {
        List<CouponDto?> list = new();
        ResponseDto? responseDto = await _couponService.GetAllCouponAsync();
        if (responseDto!=null && responseDto.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Result));
        }
        else
        {
            TempData["error"] = responseDto?.Message;

        }
        
        
        return View(list);
    }
    [HttpPost]
    public async Task<IActionResult> CouponCreate(CouponDto model)
    {
        if (ModelState.IsValid)
        {
            ResponseDto? responseDto = await _couponService.CreateCouponAsync(model);
            if (responseDto!=null && responseDto.IsSuccess)
            {
                TempData["success"] = "Coupon Created  successfully";

                return RedirectToAction(nameof(CouponIndex));
            }    else
            {
                TempData["error"] = responseDto?.Message;

            }
        }
        return View(model);
    }
    public async Task<IActionResult> CouponCreate() 
    {
        return View();
    }
    public async Task<IActionResult> CouponDelete(int couponId)
    {
        ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);

        if (response != null && response.IsSuccess)
        {
            CouponDto? model= JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
            return View(model);
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CouponDelete(CouponDto couponDto)
    {
        ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Coupon deleted successfully";
            return RedirectToAction(nameof(CouponIndex));
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return View(couponDto);
    }

}
   

