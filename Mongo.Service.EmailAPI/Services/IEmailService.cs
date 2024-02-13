using Mongo.Service.EmailAPI.Models.Dto;

namespace Mongo.Service.EmailAPI.Services;

public interface IEmailService
{
    Task EmailCartAndLog(CartDto cartDto);
    
}