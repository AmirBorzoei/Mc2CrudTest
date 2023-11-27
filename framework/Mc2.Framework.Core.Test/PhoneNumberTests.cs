using Mc2.Framework.Core.Exception;
using Mc2.Framework.Core.ValueObject;

namespace Mc2.Framework.Core.Test;

public class PhoneNumberTests
{
    public const string ValidPhoneNumber1 = "+31651022945";
    public const string ValidPhoneNumber2 = "+31651022946";
    public const string InvalidPhoneNumber1 = "+1";

    [Fact]
    public void PhoneNumberWithValidFormatShouldBeCreatedTest()
    {
        PhoneNumber phoneNumber = new(ValidPhoneNumber1);
        Assert.NotNull(phoneNumber);
    }

    [Fact]
    public void CreatingPhoneNumberWithInvalidFormatShouldThrowExceptionTest()
    {
        Assert.Throws<InvalidInputDataException>(() => new PhoneNumber(InvalidPhoneNumber1));
    }

    [Fact]
    public void TwoPhoneNumbersWithSameValuesShouldBeEqualsTest()
    {
        PhoneNumber phoneNumber1 = new(ValidPhoneNumber1);
        PhoneNumber phoneNumber2 = new(ValidPhoneNumber1);

        bool isEquals = phoneNumber1.Equals(phoneNumber2);

        Assert.True(isEquals);
    }

    [Fact]
    public void TwoPhoneNumbersWithDifferentValuesShouldNotBeEqualsTest()
    {
        PhoneNumber phoneNumber1 = new(ValidPhoneNumber1);
        PhoneNumber phoneNumber2 = new(ValidPhoneNumber2);

        bool isEquals = phoneNumber1.Equals(phoneNumber2);

        Assert.False(isEquals);
    }
}