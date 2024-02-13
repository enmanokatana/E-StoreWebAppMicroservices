using AutoMapper;
using Mongo.Services.ShoppingCart.Models;
using Mongo.Services.ShoppingCart.Models.Dto;

namespace Mongo.Services.ShoppingCart;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(
            config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>();
                config.CreateMap<CartHeaderDto, CartHeader>();
                config.CreateMap<CartDetailsDto, CartDetails>();

                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
        return mappingConfig;
    } 
}