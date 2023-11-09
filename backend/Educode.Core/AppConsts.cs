using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppConsts
    {
       public static class User
        {
            public const double TokenExpiryDays = 7; 
            public const double RefreshTokenExpiryDays = 30;

            public static class Roles
            {
                public const string Admin = "admin";
                public const string User = "user"; 
            } 
        }
    }
}
