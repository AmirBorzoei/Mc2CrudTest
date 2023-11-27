using Mc2.Framework.Validation;

namespace Mc2.Framework.Core.Test;

public class MobileValidatorTests
{
    [Theory]
    [InlineData("+982188776655", false)]
    [InlineData("+31651022945", true)]
    public void MobileValidationTest_WithExpectedResult(string phoneNumber, bool expectedResult)
    {
        bool testResult = MobileNumberValidator.IsValid(phoneNumber);

        Assert.Equal(expectedResult, testResult);
    }
}