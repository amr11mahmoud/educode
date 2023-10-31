using Educode.Application.Abstractions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Application.Services.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c=> c.FirstName).NotEmpty("FirstName");
            RuleFor(c=> c.LastName).NotEmpty("LastName");
            RuleFor(c=> c.Email).NotEmpty("Email");
            RuleFor(c=> c.Password).NotEmpty("Password");
        }
    }
}
