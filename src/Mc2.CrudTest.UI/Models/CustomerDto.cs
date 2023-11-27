using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.UI.Models;

public class CustomerDto
{
    public Guid Id { get; set; }

    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string Lastname { get; set; } = null!;

    [Required]
    [Range(typeof(DateOnly), "1900/01/01", "2023/01/01")]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string BankAccountNumber { get; set; } = null!;
}