namespace Mc2.CrudTest.Domain.Customer.DomainService;

public interface ICustomerUniqueCheckService
{
    Task CheckAsync(Guid id, string firstname, string lastname, DateOnly dateOfBirth, CancellationToken cancellationToken);
}