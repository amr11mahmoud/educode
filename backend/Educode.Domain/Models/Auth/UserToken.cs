﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Domain.Models.Auth
{
    public class UserToken: IdentityUserToken<string>
    {
    }
}