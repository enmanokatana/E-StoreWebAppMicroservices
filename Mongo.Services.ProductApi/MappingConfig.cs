using AutoMapper;
using Mongo.Services.ProductApi.Models;
using Mongo.Services.ProductApi.Models.Dto;

namespace Mongo.Services.ProductApi;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(
            config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
        return mappingConfig;
    } 
}