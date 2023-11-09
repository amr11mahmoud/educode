using Educode.Application.Services.Abstract.Commands;
using Educode.Domain.Shared;
using MediatR;
using System.Text.Json.Serialization;

namespace Educode.Application.Services.Users.Commands.CreateUser
{
    public class CreateUserCommand : ICreateCommand<Guid>
    {
        public string? Email { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
