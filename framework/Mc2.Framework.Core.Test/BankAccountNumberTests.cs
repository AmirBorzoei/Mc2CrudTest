using Mc2.Framework.Core.Exception;
using Mc2.Framework.Core.ValueObject;

namespace Mc2.Framework.Core.Test;

public class BankAccountNumberTests
{
    public const string ValidBankAccountNumber1 = "11111";
    public const string ValidBankAccountNumber2 = "22222";
    public const string InvalidBankAccountNumber1 = "11";

    [Fact]
    public void BankAccountNumberWithValidLengthShouldBeCreatedTest()
    {
        BankAccountNumber bankAccountNumber = new(ValidBankAccountNumber1);
        Assert.NotNull(bankAccountNumber);
    }

    [Fact]
    public void CreatingBankAccountNumberWithInvalidLengthShouldThrowExceptionTest()
    {
        Assert.Throws<InvalidInputDataException>(() => new BankAccountNumber(InvalidBankAccountNumber1));
    }

    [Fact]
    public void TwoBankAccountNumbersWithSameValuesShouldBeEqualsTest()
    {
        BankAccountNumber bankAccountNumber1 = new(ValidBankAccountNumber1);
        BankAccountNumber bankAccountNumber2 = new(ValidBankAccountNumber1);

        bool isEquals = bankAccountNumber1.Equals(bankAccountNumber2);

        Assert.True(isEquals);
    }

    [Fact]
    public void TwoBankAccountNumbersWithDifferentValuesShouldNotBeEqualsTest()
    {
        BankAccountNumber bankAccountNumber1 = new(ValidBankAccountNumber1);
        BankAccountNumber bankAccountNumber2 = new(ValidBankAccountNumber2);

        bool isEquals = bankAccountNumber1.Equals(bankAccountNumber2);

        Assert.False(isEquals);
    }
}