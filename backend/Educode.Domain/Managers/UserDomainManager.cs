using Educode.Core.Commands.Auth.RegisterUserCommand;
using Educode.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Domain.Managers
{
    public class UserDomainManager
    {

        public User RegisterUser(RegisterUserCommand user)
        {
            var registeredUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return registeredUser;
        }
    }
}
