using Educode.Domain.Shared;
using static Educode.Domain.Shared.Error;

namespace Educode.Domain.Users.ValueObjects
{
    public class Email : CSharpFunctionalExtensions.ValueObject
    {
        public string Value { get; private set; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string emailInput)
        {
            if (string.IsNullOrEmpty(emailInput))
            {
                return Result.Failure<Email>(Errors.General.IsRequiredError("Email"));
            }

            Email email = new Email(emailInput);

            return Result.Success(email);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
