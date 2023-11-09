using Educode.Domain.Managers;
using Educode.Domain.Shared;
using Educode.Domain.Users.Models;
using MediatR;

namespace Educode.Application.Services.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Guid>>
    {
        private readonly UserManager _userManager;

        public UpdateUserCommandHandler(UserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return Result.Failure<Guid>(Error.Errors.General.RequestIsNull(nameof(request)));

            User? user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null) return Result.Failure<Guid>(Error.Errors.Users.UserNotFound());

            var result = User.UpdateUserObject(user, request.FirstName, request.LastName, user.Email);

            var updateUserResult = await _userManager.UpdateUserAsync(result.Value);

            if (updateUserResult.IsFailure) return Result.Failure<Guid>(updateUserResult.Error);

            return Result.Success(user.Id);
        }
    }
}
