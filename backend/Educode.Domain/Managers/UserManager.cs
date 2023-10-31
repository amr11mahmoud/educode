using Educode.Domain.Shared;
using Educode.Domain.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static Educode.Domain.Shared.Error;

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

        public async Task<Result<User>> RegisterUserAsync(User user, string password)
        {
            string hashedPassword = PasswordHasher.HashPassword(user, password);

            Result setPasswordResult = User.SetUserPassword(user, hashedPassword);

            if (setPasswordResult.IsFailure) return Result.Failure<User>(setPasswordResult.Error);

            IdentityResult createUserResult = await CreateAsync(user);

            if (!createUserResult.Succeeded)
            {

                string errorCode = createUserResult.Errors.FirstOrDefault() is null ? "NULL" : createUserResult.Errors.FirstOrDefault().Code;
                string errorMsg = createUserResult.Errors.FirstOrDefault() is null ? "NULL" : createUserResult.Errors.FirstOrDefault().Description;

                return Result.Failure<User>(Errors.Users.RegisterUserError(errorCode, errorMsg));
            }

            return Result.Success(user);
        }
    }
}
