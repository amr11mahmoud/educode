using Educode.Application.Services.Abstract.Commands;
using System.Text.Json.Serialization;

namespace Educode.Application.Services.Users.Commands.UpdateUser
{


    public class UpdateUserCommand : IUpdateCommand<Guid>
    {
        public string? Email { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
