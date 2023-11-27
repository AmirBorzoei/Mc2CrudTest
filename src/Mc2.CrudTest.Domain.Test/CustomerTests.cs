using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.DomainService;
using Mc2.Framework.Core.ValueObject;
using Mc2.Framework.Validation;
using Moq;

namespace Mc2.CrudTest.Domain.Test;

public class CustomerTests
{
    public const string ValidFirstname = "Amir";
    public const string ValidLastname = "Borzoei";
    public BankAccountNumber ValidBankAccountNumber = new("11111");
    public DateOnly ValidDateOfBirthDateOnly = new(2000, 1, 1);
    public EMail ValidEMail = new("Amir.Borzoei@gmail.com");
    public PhoneNumber ValidPhoneNumber = new("+31651022945", new GoogleLibPhoneNumberValidator());

    [Fact]
    public void CustomerWithValidValuesShouldCreateSuccessfulTest()
    {
        Mock<ICustomerUniqueCheckService> customerUniqueCheckService = new();
        customerUniqueCheckService.Setup(e => e.CheckAsync(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        Mock<IEMailUniqueCheckService> eMailUniqueCheckService = new();
        eMailUniqueCheckService.Setup(e => e.CheckAsync(
                It.IsAny<Guid>(),
                It.IsAny<EMail>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        Mock<ICustomerRepository> customerRepository = new();
        customerRepository.Setup(e => e.IsDuplicateUniqueInfoAsync(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(false));
        customerRepository.Setup(e => e.IsDuplicateEMailAsync(
                It.IsAny<Guid>(),
                It.IsAny<EMail>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(false));

        CustomerBuilder customerBuilder = new(customerRepository.Object,
            customerUniqueCheckService.Object,
            eMailUniqueCheckService.Object);

        customerBuilder.SetFirstname(ValidFirstname);
        customerBuilder.SetLastname(ValidLastname);
        customerBuilder.SetDateOfBirth(ValidDateOfBirthDateOnly);
        customerBuilder.SetPhoneNumber(ValidPhoneNumber);
        customerBuilder.SetEMail(ValidEMail);
        customerBuilder.SetBankAccountNumber(ValidBankAccountNumber);

        Customer.Customer customer = customerBuilder.Build();

        Assert.NotNull(customer);
    }
}