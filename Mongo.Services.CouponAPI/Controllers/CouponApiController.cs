using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mongo.Services.CouponAPI.Data;
using Mongo.Services.CouponAPI.Models;
using Mongo.Services.CouponAPI.Models.Dto;

namespace Mongo.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponApiController(AppDbContext db ,IMapper mapper
            )
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch(Exception ex) {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("{id}")]
        public ResponseDto Get(int id) {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(obj);

              
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response; 
        }
    
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto Get(string code) {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower()==code.ToLower());
                if (obj==null)
                {
                    _response.IsSuccess = false;
                }
                _response.Result = _mapper.Map<CouponDto>(obj);

              
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response; 
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] CouponDto couponDto) {
            try
            {
                Coupon obj  = _mapper.Map<Coupon>(couponDto); 
                _db.Coupons.Add(obj);
                _db.SaveChanges();
                
                
                _response.Result = _mapper.Map<CouponDto>(obj);

              
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response; 
            
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] CouponDto couponDto) {
            try
            {
                Coupon obj  = _mapper.Map<Coupon>(couponDto); 
                _db.Coupons.Update(obj);
                _db.SaveChanges();
                
                
                _response.Result = _mapper.Map<CouponDto>(obj);

              
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response; 
        }
        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id) {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response; 
        }
    }
}
