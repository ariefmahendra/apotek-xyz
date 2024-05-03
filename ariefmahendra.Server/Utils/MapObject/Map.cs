using System.Net;
using WebResponse = ariefmahendra.Dtos.WebResponse;

namespace ariefmahendra.Utils.MapObject;

public class Map
{
    public static WebResponse MapToResponse(HttpStatusCode code, string message, object? data)
    {
        return new WebResponse()
        {
            Code = Convert.ToInt32(code),
            Message = message,
            Data = data
        };
    }
}