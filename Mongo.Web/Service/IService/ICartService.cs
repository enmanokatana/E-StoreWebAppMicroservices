using Mongo.Web.Models;

namespace Mongo.Web.Service.IService;

public interface ICartService
{
   Task<ResponseDto?> GetCartByUserIdAsync(string userId);
   Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
   Task<ResponseDto?> RemoveCartAsync(int cartHeaderId);
   Task<ResponseDto?> ApplyCoupon(CartDto cartDto);

   Task<ResponseDto?> EmailCart(CartDto cartDto);

}