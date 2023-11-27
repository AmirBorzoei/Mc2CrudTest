using System.Text.RegularExpressions;
using Mc2.Framework.Core.Domain;
using Mc2.Framework.Core.Exception;

namespace Mc2.Framework.Core.ValueObject;

public class PhoneNumber : ValueObject<PhoneNumber>
{
    public const string PhoneNumberPattern = "\\d{2}|\\s+^[A-Za-z]";

    public PhoneNumber(string phoneNumber, IPhoneNumberValidator? externalValidator = null)
    {
        CheckDefaultValidations(phoneNumber);

        externalValidator?.Validate(phoneNumber);

        Value = phoneNumber;
    }

    public string Value { get; }

    public override bool Equals(PhoneNumber other)
    {
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    private static void CheckDefaultValidations(string phoneNumber)
    {
        Regex validatePhoneNumberRegex = new(PhoneNumberPattern);
        bool isMatch = validatePhoneNumberRegex.IsMatch(phoneNumber);

        if (!isMatch)
        {
            throw new InvalidInputDataException($"[{phoneNumber}] isn't in a valid phone number format.");
        }
    }
}