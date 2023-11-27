using Mc2.CrudTest.Domain.Contract.DomainException;
using Mc2.CrudTest.Domain.Customer.DomainService;

namespace Mc2.CrudTest.DomainService.Customer;

public class CustomerUniqueCheckService : ICustomerUniqueCheckService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerUniqueCheckService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task CheckAsync(Guid id, string firstname, string lastname, DateOnly dateOfBirth, CancellationToken cancellationToken)
    {
        bool isDuplicateUniqueInfo = await _customerRepository.IsDuplicateUniqueInfoAsync(id, firstname, lastname, dateOfBirth, cancellationToken);
        if (isDuplicateUniqueInfo)
        {
            throw new DuplicationInCustomerUniqueInfoException("There is other customer with this info.");
        }
    }
}