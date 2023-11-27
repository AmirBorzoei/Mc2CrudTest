namespace Mc2.Framework.Core.Exception;

public class DomainException : System.Exception
{
    public DomainException(int errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }

    public int ErrorCode { get; init; }
}