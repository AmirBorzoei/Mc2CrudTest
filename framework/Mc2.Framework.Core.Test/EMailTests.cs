using Mc2.Framework.Core.Exception;
using Mc2.Framework.Core.ValueObject;

namespace Mc2.Framework.Core.Test;

public class EMailTests
{
    public const string ValidEMail1 = "A.B@gmail.com";
    public const string ValidEMail2 = "Amir.Borzoei@gmail.com";
    public const string InvalidEMail1 = "www.Google.com";

    [Fact]
    public void EMailWithValidFormatShouldBeCreatedTest()
    {
        EMail eMail = new(ValidEMail1);
        Assert.NotNull(eMail);
    }

    [Fact]
    public void CreatingEMailWithInvalidFormatShouldThrowExceptionTest()
    {
        Assert.Throws<InvalidInputDataException>(() => new EMail(InvalidEMail1));
    }

    [Fact]
    public void TwoEMailsWithSameValuesShouldBeEqualsTest()
    {
        EMail eMail1 = new(ValidEMail1);
        EMail eMail2 = new(ValidEMail1);

        bool isEquals = eMail1.Equals(eMail2);

        Assert.True(isEquals);
    }

    [Fact]
    public void TwoEMailsWithDifferentValuesShouldNotBeEqualsTest()
    {
        EMail eMail1 = new(ValidEMail1);
        EMail eMail2 = new(ValidEMail2);

        bool isEquals = eMail1.Equals(eMail2);

        Assert.False(isEquals);
    }
}