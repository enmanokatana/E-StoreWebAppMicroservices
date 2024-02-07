using Mongo.Web.Utility;

namespace Mongo.Web.Models;

public class RequestDto
{
    public SD.ApiType ApiType { get; set; } = SD.ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
    public string AcessToken { get; set; }
}