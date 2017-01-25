using System.Text.RegularExpressions;

namespace HmrcTpvsProxy.Domain.Validators
{
    public class PayeReferenceValidator : IValidator
    {
        private const string PayeReferenceFormat = "^[0-9]{3}[\\\\]{1}[A-Z]{1,2}[0-9]{3,5}$";

        public bool Validate(string value)
        {
            var regex = new Regex(PayeReferenceFormat);

            return regex.IsMatch(value);
        }
    }
}