using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Educode.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog
                (( HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfig) => {
                    loggerConfig.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services);
                });

            // Register Services
            builder.Services.AddControllers();
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

            CompositionRoot.CompositionRoot.InjectDependencies(builder.Services, builder.Configuration);

            var app = builder.Build();

            app.UseSerilogRequestLogging();

            // Define Middleware Pipeline 
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Educode Public API V1");
                });
            }
            else
            {
                app.UseExceptionHandler();
            }

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
    }
}