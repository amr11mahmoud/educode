using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Core.Interfaces.Users
{
    public interface IUserId
    {
        Guid UserId { get; set; }
    }
}
