namespace Mc2.CrudTest.Api.Customer.V1.Model;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string BankAccountNumber { get; set; } = null!;

    public static CustomerDto GenerateCustomerDto(Domain.Customer.Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            Firstname = customer.Firstname,
            Lastname = customer.Lastname,
            DateOfBirth = customer.DateOfBirth,
            PhoneNumber = customer.PhoneNumber.Value,
            Email = customer.Email.Value,
            BankAccountNumber = customer.BankAccountNumber.Value
        };
    }
}