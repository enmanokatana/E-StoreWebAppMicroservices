using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
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
    private readonly ICartService _cartService;
    public HomeController( IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
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
   
    [Authorize]
    [HttpPost]
    [ActionName("ProductDetails")]
    public async Task<IActionResult>  ProductDetails(ProductDto productDto)
    {
        CartDto cartDto = new CartDto()
        {
            CartHeader = new CartHeaderDto()
            {
                UserId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value
            }
        };
        CartDetailsDto cartDetailsDto = new CartDetailsDto()
        {
            Count = productDto.Count,
            ProductId = productDto.ProductId
        };
        List<CartDetailsDto> cartDetailsDtos = new() { cartDetailsDto };
        cartDto.CartDetails = cartDetailsDtos;
        
        
      
        ResponseDto? responseDto = await _cartService.UpsertCartAsync(cartDto);
        if (responseDto!=null && responseDto.IsSuccess)
        {
            TempData["success"] = "Item has been added to cart !!";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = responseDto?.Message;

        }
        
        
        return View(productDto);    }

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