namespace Recommendations.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityType, object key)
        : base($"The entity of type '{entityType}' with key '{key}' was not found")
    {
        
    }
}