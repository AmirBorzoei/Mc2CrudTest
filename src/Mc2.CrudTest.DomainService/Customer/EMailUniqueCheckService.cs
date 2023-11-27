using Mc2.CrudTest.Domain.Contract.DomainException;
using Mc2.CrudTest.Domain.Customer.DomainService;
using Mc2.Framework.Core.ValueObject;

namespace Mc2.CrudTest.DomainService.Customer;

public class EMailUniqueCheckService : IEMailUniqueCheckService
{
    private readonly ICustomerRepository _customerRepository;

    public EMailUniqueCheckService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task CheckAsync(Guid id, EMail eMail, CancellationToken cancellationToken)
    {
        bool isDuplicateEMail = await _customerRepository.IsDuplicateEMailAsync(id, eMail, cancellationToken);
        if (isDuplicateEMail)
        {
            throw new DuplicationInCustomerEMailException($"There are other customer with this EMail [{eMail.Value}].");
        }
    }
}