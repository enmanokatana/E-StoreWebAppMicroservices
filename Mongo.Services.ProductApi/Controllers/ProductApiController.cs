using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mongo.Services.ProductApi.Data;
using Mongo.Services.ProductApi.Models;
using Mongo.Services.ProductApi.Models.Dto;
using Mongo.Services.ProductApi.Utility;

namespace Mongo.Services.ProductApi.Controllers;
[Route("api/product")]
[ApiController]
[Authorize]

public class ProductApiController : ControllerBase
{
    private readonly AppDbContext _db;
    private IMapper _mapper;
    private ResponseDto _response;

    public ProductApiController(AppDbContext db, IMapper mapper)
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
            IEnumerable<Product> objList = _db.Products.ToList();
            _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
        
    }

    [HttpGet]
    [Route("{id}")]
    public ResponseDto Get(int id)
    {
        try
        {
            Product obj = _db.Products.First(u => u.ProductId == id);
            _response.Result = _mapper.Map<ProductDto>(obj);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message; 
        }

        return _response;
        
    }

    [HttpPost]
    [Authorize(Roles = SD.RoleAdmin)]
    public ResponseDto Post([FromBody] ProductDto productDto)
    {
        try
        {
            Product obj = _mapper.Map<Product>(productDto);
            _db.Products.Add(obj);
            _db.SaveChanges();
            _response.Result = _mapper.Map<ProductDto>(obj);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }

    [HttpPut]
    [Authorize(Roles = SD.RoleAdmin)]

    public ResponseDto Put([FromBody] ProductDto productDto)
    {
        try
        {
            Product obj = _mapper.Map<Product>(productDto);
            _db.Products.Update(obj);
            _db.SaveChanges();
            _response.Result = _mapper.Map<ProductDto>(obj);
            
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }

    [HttpDelete]
    [Authorize(Roles = SD.RoleAdmin)] 
    [Route("{id}")]
    public ResponseDto Delete(int id)
    {
        try
        {
            Product obj = _db.Products.FirstOrDefault(u => u.ProductId == id);
            _db.Products.Remove(obj);
            _db.SaveChanges();
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
}