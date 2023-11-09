using Educode.Core.Dtos.Users;
using Educode.Domain.Shared;
using MediatR;

namespace Educode.Application.Services.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Result<UserJwtToken>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
