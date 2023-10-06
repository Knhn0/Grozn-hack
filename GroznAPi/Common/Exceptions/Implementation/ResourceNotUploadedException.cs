namespace Exceptions.Implementation;

public class ResourceNotUploadedException : BadRequestException
{
    public ResourceNotUploadedException(string message) : base(message)
    {
    }
}