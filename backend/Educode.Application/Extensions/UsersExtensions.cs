using Educode.Core.Interfaces.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Educode.Application.Extensions
{
    public static class UsersExtensions
    {
        public static IUserId AddUserId(this IUserId command, HttpContext httpContext)
        {
            var subjectId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (subjectId != null)
            {
                Guid userId;
                bool userIdParsedSuccessfully = Guid.TryParse(subjectId.Value, out userId);

                if (userIdParsedSuccessfully)
                    command.UserId = userId;
            }

            return command;
        }
    }
}
