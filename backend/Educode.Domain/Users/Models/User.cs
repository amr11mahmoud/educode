using Educode.Domain.Shared;
using Educode.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Identity;
using static Educode.Domain.Shared.Error;

namespace Educode.Domain.Users.Models
{
    public class User : IdentityUser<Guid>
    {
        private const byte NameMinLength = 4;
        private const byte NameMaxLength = 32;

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;

        private User()
        {

        }

        public static Result<User> Create(string fName, string lName, Email email)
        {
            Result nameResult = IsValidName(fName, lName);

            if (nameResult.IsFailure)
            {
                return Result.Failure<User>(nameResult.Error);
            }

            User user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = fName,
                LastName = lName,
                Email = email.Value,
                UserName = Guid.NewGuid().ToString()
            };

            return Result.Success(user);
        }

        private static Result IsValidName(string fName, string lName)
        {
            if (string.IsNullOrEmpty(fName)) return Result.Failure(Errors.General.IsRequiredError("First Name"));
            if (string.IsNullOrEmpty(lName)) return Result.Failure(Errors.General.IsRequiredError("Last Name"));

            if (fName.Length > NameMaxLength || fName.Length < NameMinLength) 
                return Result.Failure(Errors.General.LengthError("First Name", NameMinLength, NameMaxLength));

            if (lName.Length > NameMaxLength || lName.Length < NameMinLength)
                return Result.Failure(Errors.General.LengthError("Last Name", NameMinLength, NameMaxLength));

            return Result.Success();
        }
    }
}
