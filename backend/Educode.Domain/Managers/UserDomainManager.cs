using Educode.Core.Commands.Auth.RegisterUserCommand;
using Educode.Domain.Models.Auth;
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


        public User RegisterUser(RegisterUserCommand user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var registeredUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return registeredUser;
        }
    }
}
