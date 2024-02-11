using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mongo.Web.Models;
using Mongo.Web.Service;
using Mongo.Web.Service.IService;
using Newtonsoft.Json;

namespace Mongo.Web.Controllers;

public class HomeController : Controller
{
    private IProductService _productService;

    public HomeController( IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult>  Index()
    {
        List<ProductDto?> list = new();
        ResponseDto? responseDto = await _productService.GetAllProductAsync();
        if (responseDto!=null && responseDto.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Result));
        }
        else
        {
            TempData["error"] = responseDto?.Message;

        }
        
        
        return View(list);
        
    }
    
    [Authorize]
    public async Task<IActionResult>  ProductDetails(int productId)
    {
        ProductDto? obj = new();
        ResponseDto? responseDto = await _productService.GetProductByIdAsync(productId);
        if (responseDto!=null && responseDto.IsSuccess)
        {
            obj = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
        }
        else
        {
            TempData["error"] = responseDto?.Message;

        }
        
        
        return View(obj);    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}