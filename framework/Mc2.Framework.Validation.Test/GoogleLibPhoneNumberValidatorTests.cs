using Mc2.Framework.Core.Exception;
using Mc2.Framework.Core.ValueObject;

namespace Mc2.Framework.Validation.Test;

public class GoogleLibPhoneNumberValidatorTests
{
    public const string ValidPhoneNumber1 = "+31651022945";
    public const string InvalidPhoneNumber1 = "+11";

    [Fact]
    public void ValidValueForCreatingPhoneNumberShouldBeValidateTest()
    {
        GoogleLibPhoneNumberValidator validator = new();
        PhoneNumber phoneNumber = new(ValidPhoneNumber1, validator);
        Assert.NotNull(phoneNumber);
    }

    [Fact]
    public void InvalidValueForCreatingPhoneNumberShouldBeValidateTest()
    {
        GoogleLibPhoneNumberValidator validator = new();
        InvalidInputDataException exception = Assert.Throws<InvalidInputDataException>(() =>
            new PhoneNumber(InvalidPhoneNumber1, validator));
        Assert.Contains(GoogleLibPhoneNumberValidator.InvalidInputDataExceptionMessage, exception.Message);
    }
}