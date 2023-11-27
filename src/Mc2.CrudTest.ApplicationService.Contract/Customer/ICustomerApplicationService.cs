using Mc2.CrudTest.ApplicationService.Contract.Customer.Command;

namespace Mc2.CrudTest.ApplicationService.Contract.Customer;

public interface ICustomerApplicationService
{
    Task<Guid> CreateCustomerAsync(CreateCustomerCommand command, CancellationToken cancellationToken);
    Task UpdateCustomerAsync(Guid id, UpdateCustomerCommand command, CancellationToken cancellationToken);
    Task DeleteCustomerAsync(DeleteCustomerCommand command, CancellationToken cancellationToken);
}