using Mc2.Framework.Core.Exception;
using Mc2.Framework.Core.ValueObject;
using PhoneNumbers;
using PhoneNumber = PhoneNumbers.PhoneNumber;

namespace Mc2.Framework.Validation;

public class GoogleLibPhoneNumberValidator : IPhoneNumberValidator
{
    public const string InvalidInputDataExceptionMessage = "Invalid Mobile Number";

    public void Validate(string phoneNumberValue)
    {
        if (!MobileNumberValidator.IsValid(phoneNumberValue))
        {
            throw new InvalidInputDataException(InvalidInputDataExceptionMessage);
        }
    }
}

public static class MobileNumberValidator
{
    public static bool IsValid(string phoneNumberValue)
    {
        PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        try
        {
            PhoneNumber phoneNumber = phoneNumberUtil.Parse(phoneNumberValue, "NL");
            if (!phoneNumberUtil.IsValidNumberForRegion(phoneNumber, "NL"))
            {
                return false;
            }

            return true;
        }
        catch (NumberParseException)
        {
            return false;
        }
    }
}