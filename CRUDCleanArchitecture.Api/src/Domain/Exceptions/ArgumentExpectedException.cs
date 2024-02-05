namespace CRUDCleanArchitecture.Domain.Exceptions;
public class ArgumentExpectedException : Exception
{
    public ArgumentExpectedException(string message) : base(message)
    {
    }
}
