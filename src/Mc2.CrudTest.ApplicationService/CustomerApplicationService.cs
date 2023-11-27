using Mc2.CrudTest.ApplicationService.Contract.Customer;
using Mc2.CrudTest.ApplicationService.Contract.Customer.Command;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.DomainService;
using Mc2.Framework.Core.ValueObject;

namespace Mc2.CrudTest.ApplicationService;

public class CustomerApplicationService : ICustomerApplicationService
{
    private readonly CustomerBuilder _customerBuilder;
    private readonly ICustomerRepository _repository;

    public CustomerApplicationService(CustomerBuilder customerBuilder,
        ICustomerRepository repository)
    {
        _customerBuilder = customerBuilder;
        _repository = repository;
    }

    public async Task<Guid> CreateCustomerAsync(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        Guid newCustomerId = Guid.NewGuid();
        _customerBuilder.SetId(newCustomerId);
        _customerBuilder.SetFirstname(command.Firstname!);
        _customerBuilder.SetLastname(command.Lastname!);
        _customerBuilder.SetDateOfBirth(command.DateOfBirth);
        _customerBuilder.SetPhoneNumber(new PhoneNumber(command.PhoneNumber!));
        _customerBuilder.SetEMail(new EMail(command.Email!.ToLower()));
        _customerBuilder.SetBankAccountNumber(new BankAccountNumber(command.BankAccountNumber!));
        Customer newCustomer = _customerBuilder.Build();

        await _repository.SaveAsync(newCustomer, cancellationToken);

        return newCustomerId;
    }

    public async Task UpdateCustomerAsync(Guid id, UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        _customerBuilder.SetId(id);
        _customerBuilder.SetFirstname(command.Firstname!);
        _customerBuilder.SetLastname(command.Lastname!);
        _customerBuilder.SetDateOfBirth(command.DateOfBirth);
        _customerBuilder.SetPhoneNumber(new PhoneNumber(command.PhoneNumber!));
        _customerBuilder.SetEMail(new EMail(command.Email!.ToLower()));
        _customerBuilder.SetBankAccountNumber(new BankAccountNumber(command.BankAccountNumber!));
        Customer customer = _customerBuilder.Build();

        await _repository.UpdateAsync(customer, cancellationToken);
    }

    public async Task DeleteCustomerAsync(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(command.Id, cancellationToken);
    }
}