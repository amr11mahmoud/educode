using Educode.Domain.Models.Auth;
using Educode.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Educode.CompositionRoot
{
    public class CompositionRoot
    {
        public CompositionRoot()
        {

        }

        public static void InjectDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString: configuration.GetConnectionString("Default"), 
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }, 2048 /* pool size */ );

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<User, Role, ApplicationDbContext, string>>()
                .AddRoleStore<RoleStore<Role, ApplicationDbContext, string>>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserValidator<UserValidator<User>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddPasswordValidator<PasswordValidator<User>>()
                .AddSignInManager<SignInManager<User>>()
                .AddErrorDescriber<IdentityErrorDescriber>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt=>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["Authentication:JwtBearer:ValidAudience"],
                    ValidIssuer = configuration["Authentication:JwtBearer:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"]))
                };
            });
        }
    }
}
