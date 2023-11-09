using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Core.Dtos.Users
{
    public class CreateUserResponseDto
    {
        public Guid UserId { get; private set; }

        public CreateUserResponseDto(Guid userId)
        {
            UserId = userId;
        }
    }
}
