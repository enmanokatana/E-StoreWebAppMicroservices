using Mongo.Services.ShoppingCart.Models.Dto;

namespace Mongo.Services.ShoppingCart.Service.IService;

public interface IProductService
{
   Task<IEnumerable<ProductDto>> GetProducts();
   
}