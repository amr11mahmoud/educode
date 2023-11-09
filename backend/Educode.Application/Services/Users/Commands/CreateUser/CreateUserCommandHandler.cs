using Educode.Domain.Managers;
using Educode.Domain.Shared;
using Educode.Domain.Users.Models;
using MediatR;

namespace Educode.Application.Services.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly UserManager _userManager;

        public CreateUserCommandHandler(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return Result.Failure<Guid>(Error.Errors.General.RequestIsNull(nameof(request)));

            Result<User> userResult = User.Create(request.FirstName, request.LastName, request.Email);

            if (userResult.IsFailure) return Result.Failure<Guid>(userResult.Error);

            Result<User> registerUserResult = await _userManager.RegisterUserAsync(userResult.Value, request.Password);
            if (registerUserResult.IsFailure) return Result.Failure<Guid>(registerUserResult.Error);

            return Result.Success(registerUserResult.Value.Id);
        }
    }
}
