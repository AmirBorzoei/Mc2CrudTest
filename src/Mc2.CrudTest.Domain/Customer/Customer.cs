using Mc2.CrudTest.Domain.Customer.DomainService;
using Mc2.Framework.Core.Domain;
using Mc2.Framework.Core.ValueObject;

namespace Mc2.CrudTest.Domain.Customer;

public class Customer : Entity, IAggregateRoot
{
    private Customer(Guid id,
        string firstname,
        string lastname,
        DateOnly dateOfBirth,
        PhoneNumber phoneNumber,
        EMail email,
        BankAccountNumber bankAccountNumber)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;
    }

    public Customer(Guid id,
        string firstname,
        string lastname,
        DateOnly dateOfBirth,
        PhoneNumber phoneNumber,
        EMail email,
        BankAccountNumber bankAccountNumber,
        ICustomerUniqueCheckService customerUniqueCheckService,
        IEMailUniqueCheckService eMailUniqueCheckService)
    {
        Id = id;

        //Todo: Define ValueObject
        SetUniqueInfo(id, firstname, lastname, dateOfBirth, customerUniqueCheckService);

        SetEMail(id, email, eMailUniqueCheckService);

        PhoneNumber = phoneNumber;
        BankAccountNumber = bankAccountNumber;
    }

    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public EMail Email { get; private set; }
    public BankAccountNumber BankAccountNumber { get; private set; }

    private void SetUniqueInfo(Guid id,
        string firstname,
        string lastname,
        DateOnly dateOfBirth,
        ICustomerUniqueCheckService customerUniqueCheckService)
    {
        customerUniqueCheckService.CheckAsync(id, firstname, lastname, dateOfBirth, CancellationToken.None).GetAwaiter().GetResult();

        Firstname = firstname;
        Lastname = lastname;
        DateOfBirth = dateOfBirth;
    }

    private void SetEMail(Guid id, EMail email, IEMailUniqueCheckService eMailUniqueCheckService)
    {
        eMailUniqueCheckService.CheckAsync(id, email, CancellationToken.None).GetAwaiter().GetResult();

        Email = email;
    }
}