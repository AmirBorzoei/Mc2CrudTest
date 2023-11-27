using Mc2.Framework.Core.Domain;
using Mc2.Framework.Core.Exception;

namespace Mc2.Framework.Core.ValueObject;

public class BankAccountNumber : ValueObject<BankAccountNumber>
{
    public const int MinBankAccountNumberLength = 3;
    public const int MaxBankAccountNumberLength = 17;

    public BankAccountNumber(string bankAccountNumber)
    {
        CheckLength(bankAccountNumber);

        Value = bankAccountNumber;
    }

    public string Value { get; }

    public override bool Equals(BankAccountNumber other)
    {
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    private static void CheckLength(string bankAccountNumber)
    {
        if (bankAccountNumber.Length is < MinBankAccountNumberLength or > MaxBankAccountNumberLength)
        {
            throw new InvalidInputDataException($"[{bankAccountNumber}] isn't in a valid length.");
        }
    }
}