namespace CQRS.Shared.Exceptions;

public class ExistingEntityException : Exception
{
    public ExistingEntityException(string message) : base(message)
    {

    }
}