using Educode.Application.Services.Abstract.Queries;
using Educode.Core.Dtos.Users;
using Educode.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Application.Services.Users.Queries.ReadUser
{
    public class ReadUserQuery : IReadQuery<User>
    {
        public Guid UserId { get; set; }
    }
}
