using Educode.Core.Interfaces.Commands;
using Microsoft.AspNetCore.Http;

namespace Educode.Application.Extensions
{
    public static class CommandExtensions
    {
        public static ICommandHttpContext<HttpContext> AddHttpContext(this ICommandHttpContext<HttpContext> request, HttpContext httpContext)
        {
            request.HttpContext = httpContext;
            return request;
        }
    }
}
