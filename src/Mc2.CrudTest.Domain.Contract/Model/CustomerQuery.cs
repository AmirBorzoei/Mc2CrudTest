namespace Mc2.CrudTest.Domain.Contract.Model;

public class CustomerQuery
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? BankAccountNumber { get; set; }
}