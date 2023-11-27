using Mc2.CrudTest.Domain.Contract.Model;
using Mc2.Framework.Core.ValueObject;

namespace Mc2.CrudTest.Domain.Customer.DomainService;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync(CustomerQuery query, CancellationToken cancellationToken);
    Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> IsDuplicateEMailAsync(Guid id, EMail eMail, CancellationToken cancellationToken);
    Task<bool> IsDuplicateUniqueInfoAsync(Guid id, string firstname, string lastname, DateOnly dateOfBirth, CancellationToken cancellationToken);

    Task SaveAsync(Customer customer, CancellationToken cancellationToken);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}