using Educode.Domain.Shared;
using Educode.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System.Text;
using static Educode.Domain.Shared.Error;

namespace Educode.Domain.Users.Models
{
    public class User : IdentityUser<Guid>, IComparable<User>
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

            if (nameResult.IsFailure) return Result.Failure<User>(nameResult.Error);

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

        public static Result SetUserPassword(User user, string password)
        {
            user.PasswordHash = password;

            return Result.Success();
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

        public int CompareTo(User? other)
        {
            if (other == null) return 0;
            if (other.GetType() != GetType()) return 0;

            return other.GetHashCode() == GetHashCode() ? 1 : 0;
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash = (hash * 7) + Id.GetHashCode();
            hash = (hash * 7) + Email.GetHashCode();

            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) { return false; }

            return obj.GetHashCode() == GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Id: ");
            sb.Append(Id.ToString());

            sb.Append("FirstName: ");
            sb.Append(FirstName.ToString());

            sb.Append("LastName: ");
            sb.Append(LastName.ToString());

            sb.Append("Email: ");
            sb.Append(Email.ToString());

            sb.Append("UserName: ");
            sb.Append(UserName.ToString());

            return sb.ToString();
        }
    }
}
