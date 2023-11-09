using Educode.Domain.Managers;
using Educode.Domain.Shared;
using Educode.Domain.Users.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Educode.Application.Services.Users.Commands.LogoutUser
{
    public class LogoutUserCommandHandler : BaseUserCommandHandler, IRequestHandler<LogoutUserCommand, Result<bool>>
    {
        public LogoutUserCommandHandler(SignInManager<User> signInManager, UserManager userManager) : base(signInManager, userManager)
        {
        }

        public async Task<Result<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null) return Result.Failure<bool>(Error.Errors.General.RequestIsNull(nameof(request)));

                User? user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null) return Result.Failure<bool>(Error.Errors.Users.UserNotFound());

                if (_signInManager.IsSignedIn(request.HttpContext.User))
                {
                    //TODO user sign out logic
                }

                return Result.Success(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
