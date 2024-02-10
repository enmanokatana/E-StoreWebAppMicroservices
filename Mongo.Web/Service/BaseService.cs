using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Mongo.Web.Models;
using Mongo.Web.Service.IService;
using Mongo.Web.Utility;
using Newtonsoft.Json;

namespace Mongo.Web.Service;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenProvider _tokenProvider;
    public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProvider = tokenProvider;
    }

    public async Task<ResponseDto?> SendAsync(RequestDto requestDto,bool withBearer =true)
    {
        try
        {


            HttpClient client = _httpClientFactory.CreateClient("MongoAPI");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            //token
            if (withBearer)
            {
                var token = _tokenProvider.GetToken();
                message.Headers.Add("Authorization",$"Bearer {token}");

            }
            message.RequestUri = new Uri(requestDto.Url);
            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8,
                    "application/json");
            }

            HttpResponseMessage? apiResponseMessage = null;

            switch (requestDto.ApiType)
            {
                case SD.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case SD.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case SD.ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }


            apiResponseMessage = await client.SendAsync(message);
            switch (apiResponseMessage.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new ResponseDto() { IsSuccess = false, Message = "Notfound" };
                case HttpStatusCode.Forbidden:
                    return new ResponseDto() { IsSuccess = false, Message = "Forbidden" };
                case HttpStatusCode.Unauthorized:
                    return new ResponseDto() { IsSuccess = false, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new ResponseDto() { IsSuccess = false, Message = "InternalServerError" };
                default:
                    var apiContent = await apiResponseMessage.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
            }
        }
        catch (Exception e)
        {
            var dto = new ResponseDto()
            {
                Message = e.Message.ToString(),
                IsSuccess = false
            };
            return dto;
        }
    }

}