using Mongo.Services.ShoppingCart.Models.Dto;

namespace Mongo.Services.ShoppingCart.Service.IService;

public interface ICouponService
{

    Task<CouponDto> GetCoupon(string couponCode);
}