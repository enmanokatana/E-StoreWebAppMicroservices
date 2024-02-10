using Microsoft.AspNetCore.Mvc;
using Mongo.Web.Models;
using Mongo.Web.Service.IService;
using Newtonsoft.Json;

namespace Mongo.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
        
    }

    public async Task<IActionResult> ProductIndex()
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

    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }

    public async Task<IActionResult> ProductCreate(ProductDto model)
    {
        if (ModelState.IsValid)
        {
            ResponseDto? responseDto = await _productService.CreateProductAsync(model);
            if (responseDto!=null && responseDto.IsSuccess)
            {
                TempData["success"] = " Product Created  successfully";

                return RedirectToAction(nameof(ProductIndex));
            }    else
            {
                TempData["error"] = responseDto?.Message;

            }
        }

        return View(model);
    }

    public async Task<IActionResult> ProductDelete(ProductDto productDto)
    {
        ResponseDto? response = await _productService.DeleteProductAsync(productDto.ProductId);
        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Coupon deleted successfully";
            return RedirectToAction(nameof(ProductIndex));
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return View(productDto);
    }

    public async Task<IActionResult> ProductUpdate(ProductDto productDto)
    {
        ResponseDto? response = await _productService.UpdateProductAsync(productDto);
        
        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Coupon deleted successfully";
            return RedirectToAction(nameof(ProductIndex));
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return View(productDto);
        
        
        
    }
    
    
    
    
    
    
    
}