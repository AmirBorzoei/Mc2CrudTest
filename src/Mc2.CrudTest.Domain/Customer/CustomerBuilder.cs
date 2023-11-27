using Mc2.CrudTest.Domain.Contract.DomainException;
using Mc2.CrudTest.Domain.Customer.DomainService;
using Mc2.Framework.Core.Domain;
using Mc2.Framework.Core.Exception;
using Mc2.Framework.Core.ValueObject;

namespace Mc2.CrudTest.Domain.Customer;

public class CustomerBuilder : IBuilder<Customer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerUniqueCheckService _customerUniqueCheckService;
    private readonly IEMailUniqueCheckService _eMailUniqueCheckService;


    private BankAccountNumber _bankAccountNumber = null!;
    private DateOnly _dateOfBirth;
    private EMail _email = null!;
    private string _firstname = null!;
    private Guid _id;
    private string _lastname = null!;
    private PhoneNumber _phoneNumber = null!;

    public CustomerBuilder(ICustomerRepository customerRepository,
        ICustomerUniqueCheckService customerUniqueCheckService,
        IEMailUniqueCheckService eMailUniqueCheckService)
    {
        _customerRepository = customerRepository;
        _eMailUniqueCheckService = eMailUniqueCheckService;
        _customerUniqueCheckService = customerUniqueCheckService;
    }

    public Customer Build()
    {
        CheckValidation();

        return new Customer(_id,
            _firstname,
            _lastname,
            _dateOfBirth,
            _phoneNumber,
            _email,
            _bankAccountNumber,
            _customerUniqueCheckService,
            _eMailUniqueCheckService);
    }

    public CustomerBuilder SetId(Guid id)
    {
        _id = id;
        return this;
    }

    public CustomerBuilder SetFirstname(string firstname)
    {
        _firstname = firstname;
        return this;
    }

    public CustomerBuilder SetLastname(string lastname)
    {
        _lastname = lastname;
        return this;
    }

    public CustomerBuilder SetDateOfBirth(DateOnly dateOfBirth)
    {
        _dateOfBirth = dateOfBirth;
        return this;
    }

    public CustomerBuilder SetPhoneNumber(PhoneNumber phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public CustomerBuilder SetEMail(EMail eMail)
    {
        _email = eMail;
        return this;
    }

    public CustomerBuilder SetBankAccountNumber(BankAccountNumber bankAccountNumber)
    {
        _bankAccountNumber = bankAccountNumber;
        return this;
    }

    private void CheckValidation()
    {
        AggregateDomainExceptions exceptions = new();

        bool isDuplicateUniqueInfo = _customerRepository
            .IsDuplicateUniqueInfoAsync(_id, _firstname, _lastname, _dateOfBirth, CancellationToken.None).GetAwaiter().GetResult();
        if (isDuplicateUniqueInfo)
        {
            exceptions.DomainExceptions.Add(new DuplicationInCustomerUniqueInfoException("There is other customer with this info."));
        }
        
        bool isDuplicateEMail = _customerRepository
            .IsDuplicateEMailAsync(_id, _email, CancellationToken.None).GetAwaiter().GetResult();
        if (isDuplicateEMail)
        {
            exceptions.DomainExceptions.Add(new DuplicationInCustomerEMailException($"There are other customer with this EMail [{_email.Value}]."));
        }

        if (exceptions.DomainExceptions.Any())
        {
            throw exceptions;
        }
    }
}