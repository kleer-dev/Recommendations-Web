namespace Recommendations.Application.Common.Exceptions;

public class RecordIsExistException : Exception
{
    public RecordIsExistException(object record)
        : base($"Record '{record}' already exist")
    {

    }
}
