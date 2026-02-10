namespace CQRS.Shared.Exceptions;

public class UnproccessableEntityException: Exception
{
    public UnproccessableEntityException(string message) : base(message)
    {
        
    }
}
