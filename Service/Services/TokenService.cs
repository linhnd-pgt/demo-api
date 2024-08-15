using Service.DTOs;
using Service.Entities;
using Service.Repositories.Base;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
namespace Service.Services
{

    public interface ITokenService
    { 
        Task<string> Login(string username, string password);

        Task RefreshToken(UserEntity user);
        Task RevokeToken(UserEntity user);


    }

    public class TokenService : ITokenService
    {

        private readonly string symetricKey = "this is my secret key for authentication";
        private readonly string url = "https://localhost:7173";
        private readonly IUserService _userService;

        public TokenService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _userService.GetUserLoggedIn(username, password);

            if (user is not null)
            {

                if (user.ActiveFlag == false)
                    throw new Exception($"Account {user.UserName} has been deactivated");
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                var accessToken = this.GenerateAccessToken(claims);
                var refreshToken = this.GenerateRefreshToken();

                user.AccessToken = accessToken;
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1);

                await _userService.UpdateUserWithEntity(user);

                return accessToken;


            }
            else
            {
                throw new Exception("Username or password is not correct");
            }

        }

        public async Task RefreshToken(UserEntity userModel)
        {
            string? accessToken = userModel.AccessToken;
            string? refreshToken = userModel.RefreshToken;
            ClaimsPrincipal principal = new ClaimsPrincipal();
            if (accessToken != null)
                principal = this.GetPrincipalFromExpiredToken(accessToken);

            var user = await _userService.GetUserLoggedIn(userModel.Email, userModel.PasswordHash);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new Exception("User is not valid");

            var newAccessToken = this.GenerateAccessToken(principal.Claims);
            var newRefreshToken = this.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userService.UpdateUserWithEntity(user);
        }

        public async Task RevokeToken(UserEntity userModel)
        {
            var user = await _userService.GetUserByUsername(userModel.UserName);
            if (user == null) throw new Exception($"User {user.UserName} is not existed");

            user.RefreshToken = null;

            await _userService.UpdateUserWithEntity(user);
        }

        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symetricKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: url,
                audience: url,
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }



        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symetricKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Verification failed");

            return principal;
        }
    }
}
