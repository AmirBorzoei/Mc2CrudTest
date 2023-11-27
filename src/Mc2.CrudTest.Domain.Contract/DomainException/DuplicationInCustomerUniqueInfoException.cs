namespace Mc2.CrudTest.Domain.Contract.DomainException;

public class DuplicationInCustomerUniqueInfoException : Framework.Core.Exception.DomainException
{
    public DuplicationInCustomerUniqueInfoException(string message) : base(201, message)
    {
    }
}