using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.ApplicationService.Contract.Customer.Command;

public class CreateCustomerCommand
{
    [Required(AllowEmptyStrings = false)]
    public string? Firstname { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string? Lastname { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string? PhoneNumber { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string? Email { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string? BankAccountNumber { get; set; }
}