using HmrcTpvsProxy.Domain.Validators;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Validators
{
    [TestFixture]
    public class PayeReferenceValidatorTest
    {
        [Test]
        [TestCase("123\\12345")]
        [TestCase("123\\123ABC")]
        [TestCase("123\\ABC123")]
        [TestCase("ABC\\AA123")]
        [TestCase("123-AA123")]
        [TestCase("123\\A1A1")]
        [TestCase("123\\A123!")]
        public void GivenIHaveAnInvalidPayeReference_WhenIValidate_ThenAnInvalidResultShouldBeGenerated(string payeReference)
        {
            var validator = new PayeReferenceValidator();

            Assert.That(validator.Validate(payeReference), Is.False);
        }

        [Test]
        [TestCase("123\\A123")]
        [TestCase("123\\A12345")]
        [TestCase("123\\AB12345")]
        public void GivenIHaveAValidPayeReference_WhenIValidate_ThenAValidResultShouldBeGenerated(string payeReference)
        {
            var validator = new PayeReferenceValidator();

            Assert.That(validator.Validate(payeReference), Is.True);
        }
    }
}