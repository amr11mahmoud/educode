using Educode.Domain.Shared;
using MediatR;

namespace Educode.Application.Services.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Result<Guid>>
    {
        public string? Email { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
    }
}
