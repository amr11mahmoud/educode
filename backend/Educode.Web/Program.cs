using Educode.Domain.Managers;
using Educode.Domain.Users.Models;
using Educode.Infrastructure.DbContext;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using Educode.Domain.Shared;
using static Educode.Domain.Shared.Error;
using Educode.Web.Middlewares;
using Educode.Domain.Abstractions;
using Educode.Infrastructure.Repositories;
using Educode.Web.Configurations;

namespace Educode.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
            var applicationAssembly = typeof(Application.AssemblyReference).Assembly;

            builder.Host.UseSerilog
                ((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfig) =>
                {
                    loggerConfig.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services);
                });

            builder.Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = ModelStateValidator.ValidModelState;
                }).AddApplicationPart(presentationAssembly);

            builder.Services.AddValidatorsFromAssembly(applicationAssembly);
            builder.Services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
            builder.Services.AddFluentValidationClientsideAdapters();

            builder.Services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(applicationAssembly);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "Educode Public API v1.0", Version = "v1" });
                opt.AddSecurityDefinition("EducodeApiAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Input Valid Access token to access this API"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "EducodeApiAuth"
                            }
                        }, new List<string>()
                    }
                });
            });

            builder.Services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.ReportApiVersions = true;
            });

            builder.Services.AddDbContextPool<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString: configuration.GetConnectionString("Default"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }, 2048 /* pool size */ );

            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<User, Role, ApplicationDbContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>>()
                .AddRoleStore<RoleStore<Role, ApplicationDbContext, Guid, UserRole, RoleClaim>>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserValidator<UserValidator<User>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddPasswordValidator<PasswordValidator<User>>()
                .AddSignInManager<SignInManager<User>>()
                .AddErrorDescriber<IdentityErrorDescriber>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
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

            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddTransient<UserManager>();
            builder.Services.AddTransient(typeof(IApplicationRepository<>), typeof(ApplicationRepository<>));

            var app = builder.Build();

            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Educode Public API V1");
                });
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseResponseWrapper();
            app.UseHttpsRedirection();
            app.UseRouting();

            // app.UseRateLimiter();
            // app.UseRequestLocalization();
            //app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseResponseCaching();

            app.MapControllers();

            app.Run();
        }

        public class ModelStateValidator
        {
            public static IActionResult ValidModelState(ActionContext context)
            {
                (string fieldName, ModelStateEntry entry) = context.ModelState.First(x => x.Value.Errors.Count > 0);
                string errorSerialized = entry.Errors.First().ErrorMessage;

                try
                {
                    Error error = Deserialize(errorSerialized);

                    return new BadRequestObjectResult(error);

                }
                catch (Exception)
                {

                    return new BadRequestObjectResult(Errors.General.InvalidFieldDataType(fieldName));
                }
            }
        }
    }
}