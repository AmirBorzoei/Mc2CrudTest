namespace Mc2.CrudTest.Domain.Contract.DomainException;

public class DuplicationInCustomerEMailException : Framework.Core.Exception.DomainException
{
    public DuplicationInCustomerEMailException(string message) : base(202, message)
    {
    }
}