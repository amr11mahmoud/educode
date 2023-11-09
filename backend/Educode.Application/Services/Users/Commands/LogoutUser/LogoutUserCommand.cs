using Educode.Core.Interfaces.Commands;
using Educode.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Educode.Application.Services.Users.Commands.LogoutUser
{
    public class LogoutUserCommand : ICommandHttpContext<HttpContext>, IRequest<Result<bool>>
    {
        public Guid UserId { get; set; }
        public HttpContext HttpContext { get; set; }
    }
}
