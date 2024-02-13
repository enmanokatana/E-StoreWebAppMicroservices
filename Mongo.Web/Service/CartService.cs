﻿using Mongo.Web.Models;
using Mongo.Web.Service.IService;
using Mongo.Web.Utility;

namespace Mongo.Web.Service;

public class CartService : ICartService
{
    private readonly IBaseService _baseService;

    public CartService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.CaartApi + $"/api/cart/GetCart/"+ userId
        });
    }

    public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDto,
            Url = SD.CaartApi + $"/api/cart/CartUpsert"
        });}

    public async Task<ResponseDto?> RemoveCartAsync(int cartHeaderId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = cartHeaderId,
            Url = SD.CaartApi + $"/api/cart/RemoveCart"
        });
        
    }    

    public async Task<ResponseDto?> ApplyCoupon(CartDto cartDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDto,
            Url = SD.CaartApi + $"/api/cart/ApplyCoupon"
        });
        
    }

    public async Task<ResponseDto?> EmailCart(CartDto cartDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDto,
            Url = SD.CaartApi + $"/api/cart/EmailCartRequest"
        });

    }
}
