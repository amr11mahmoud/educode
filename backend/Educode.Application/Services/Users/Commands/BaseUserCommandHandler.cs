using Educode.Domain.Managers;
using Educode.Domain.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace Educode.Application.Services.Users.Commands
{
    public class BaseUserCommandHandler
    {
        protected readonly SignInManager<User> _signInManager;
        protected readonly UserManager _userManager;

        public BaseUserCommandHandler(SignInManager<User> signInManager, UserManager userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
    }
}
