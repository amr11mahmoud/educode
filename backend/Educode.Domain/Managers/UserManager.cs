using Educode.Core.Dtos.Users;
using Educode.Core;
using Educode.Domain.Abstractions;
using Educode.Domain.Shared;
using Educode.Domain.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Educode.Domain.Shared.Error;
using Microsoft.Extensions.Configuration;

namespace Educode.Domain.Managers
{
    public class UserManager : UserManager<User>
    {
        private IApplicationRepository<UserRefreshToken> _refreshTokenRepo;
        private readonly IConfiguration _configuration;

        public UserManager(
            IUserStore<User> userStore,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger,
            IApplicationRepository<UserRefreshToken> refreshTokenRepo,
            IConfiguration configuration
            ) : base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _refreshTokenRepo = refreshTokenRepo;
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Result<User>> RegisterUserAsync(User user, string password)
        {
            string hashedPassword = PasswordHasher.HashPassword(user, password);

            Result setPasswordResult = User.SetUserPassword(user, hashedPassword);

            if (setPasswordResult.IsFailure) return Result.Failure<User>(setPasswordResult.Error);

            IdentityResult createUserResult = await CreateAsync(user);

            if (!createUserResult.Succeeded)
            {
                string errorCode = createUserResult.Errors.FirstOrDefault() is null ? "NULL" : createUserResult.Errors.FirstOrDefault().Code;
                string errorMsg = createUserResult.Errors.FirstOrDefault() is null ? "NULL" : createUserResult.Errors.FirstOrDefault().Description;
                return Result.Failure<User>(Errors.Users.RegisterUserError(errorCode, errorMsg));
            }
            return Result.Success(user);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public new async Task<Result<User>> UpdateUserAsync(User user)
        {
            IdentityResult updateUserResult = await UpdateAsync(user);

            if (!updateUserResult.Succeeded)
            {
                string errorCode = updateUserResult.Errors.FirstOrDefault() is null ? "NULL" : updateUserResult.Errors.FirstOrDefault().Code;
                string errorMsg = updateUserResult.Errors.FirstOrDefault() is null ? "NULL" : updateUserResult.Errors.FirstOrDefault().Description;
                return Result.Failure<User>(Errors.Users.RegisterUserError(errorCode, errorMsg));
            }
            return Result.Success(user);
        }

        public async Task StoreRefreshToken(UserRefreshToken token)
        {
            await _refreshTokenRepo.AddAsync(token);
            await _refreshTokenRepo.SaveChangesAsync();
        }

        public async Task<UserJwtToken> GenerateUserTokens(User user)
        {
            return new UserJwtToken
            {
                AccessToken = GenerateToken(user),
                RefreshToken = await GenerateRefreshToken(user),
            };
        }

        private JWTToken GenerateToken(User user)
        {
            var claims = new List<Claim> {
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            string key = _configuration["Authentication:JwtBearer:SecurityKey"];

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(AppConsts.User.TokenExpiryDays);
            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtBearer:ValidIssuer"],
                audience: _configuration["Authentication:JwtBearer:ValidAudience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JWTToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        private async Task<JWTToken> GenerateRefreshToken(User user)
        {
            var claims = new List<Claim> {
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtBearer:RefreshSecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);
            var expiration = DateTime.UtcNow.AddDays(AppConsts.User.RefreshTokenExpiryDays);

            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtBearer:Issuer"],
                audience: _configuration["Authentication:JwtBearer:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            ));

            UserRefreshToken refreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                Token = token,
                ExpirationDate = expiration,
                IsUsed = false
            };

            await StoreRefreshToken(refreshToken);

            return new JWTToken
            {
                Token = token,
                Expiration = expiration
            };
        }
    }
}
