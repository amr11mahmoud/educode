using Educode.Application.Services.Users.Commands;
using Educode.Core.Dtos.Users;
using Educode.Domain.Managers;
using Educode.Domain.Shared;
using Educode.Domain.Users.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Educode.Application.Services.Users.Queries.ReadUser
{
    public class ReadUserQueryHandler : BaseUserCommandHandler, IRequestHandler<ReadUserQuery, Result<User>>
    {
        public ReadUserQueryHandler(SignInManager<User> signInManager, UserManager userManager) : base(signInManager, userManager)
        {
        }

        public async Task<Result<User>> Handle(ReadUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null) return Result.Failure<User>(Error.Errors.General.RequestIsNull(nameof(request)));

                User? user = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (user == null) return Result.Failure<User>(Error.Errors.Users.UserNotFound());

                return Result.Success(user);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
