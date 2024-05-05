using System.Runtime.Serialization;

namespace ariefmahendra.Utils.CustomException;

public class NotFoundException: Exception
{
    public NotFoundException(string? message) : base(message)
    {
    }
}