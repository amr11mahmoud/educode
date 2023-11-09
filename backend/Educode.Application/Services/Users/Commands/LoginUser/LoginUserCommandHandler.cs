using Educode.Core.Dtos.Users;
using Educode.Domain.Managers;
using Educode.Domain.Shared;
using Educode.Domain.Users.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Educode.Application.Services.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : BaseUserCommandHandler, IRequestHandler<LoginUserCommand, Result<UserJwtToken>>
    {
        public LoginUserCommandHandler(SignInManager<User> signInManager, UserManager userManager):base(signInManager, userManager)
        {
        }

        public async Task<Result<UserJwtToken>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return Result.Failure<UserJwtToken>(Error.Errors.General.RequestIsNull(nameof(request)));

            User? user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return Result.Failure<UserJwtToken>(Error.Errors.Users.UserNotFound(request.Email));

            bool canSignIn = await _signInManager.CanSignInAsync(user);
            if (!canSignIn) return Result.Failure<UserJwtToken>(Error.Errors.Users.UserCanNotSignIn());

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
            if (signInResult.IsNotAllowed) return Result.Failure<UserJwtToken>(Error.Errors.Users.UserSignInNotAllowed());
            if (signInResult.IsLockedOut) return Result.Failure<UserJwtToken>(Error.Errors.Users.UserLockout());
            if (!signInResult.Succeeded) return Result.Failure<UserJwtToken>(Error.Errors.Users.InvalidPassword());

            UserJwtToken userToken = await _userManager.GenerateUserTokens(user);

            return Result.Success(userToken);
        }
    }
}
