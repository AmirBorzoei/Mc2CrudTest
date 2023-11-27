using System.Text.RegularExpressions;
using Mc2.Framework.Core.Domain;
using Mc2.Framework.Core.Exception;

namespace Mc2.Framework.Core.ValueObject;

public class EMail : ValueObject<EMail>
{
    public const string EMailPattern = "^\\S+@\\S+\\.\\S+$";

    public EMail(string eMail)
    {
        CheckFormat(eMail);

        Value = eMail;
    }

    public string Value { get; }

    public override bool Equals(EMail other)
    {
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    private static void CheckFormat(string eMail)
    {
        Regex validateEMailRegex = new(EMailPattern);
        bool isMatch = validateEMailRegex.IsMatch(eMail);

        if (!isMatch)
        {
            throw new InvalidInputDataException($"[{eMail}] isn't in a valid eMail format.");
        }
    }
}