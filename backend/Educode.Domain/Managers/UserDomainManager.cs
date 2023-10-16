using Educode.Domain.Exceptions;
using Educode.Domain.Shared;
using Educode.Domain.Users.Models;
using Educode.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Educode.Domain.Managers
{
    public class UserManager : UserManager<User>
    {

        public UserManager(
            IUserStore<User> userStore,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger
            ) : base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }

        public Result RegisterUser(RegisterUserCommand user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            Result<Email> emailResult = Email.Create(user.Email);

            if (emailResult.IsFailure) throw new EmailException(emailResult.Error.Code);

            Result<User> userResult = User.Create(user.FirstName, user.LastName, emailResult.Value);

            if (userResult.IsFailure) throw new RegisterUserException(userResult.Error.Code);

            return userResult;
        }
    }

    public class RegisterUserCommand
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
