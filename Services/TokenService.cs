using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AuthenticationService.Models;
using AuthenticationService.Helpers;
using AuthenticationService.Exceptions;

namespace AuthenticationService.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        public TokenService(IOptions<AppSettings> appsettings)
        {
            this._appSettings = appsettings.Value;
        }

        public Account Authenticate(Account user)
        {
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }
    }

    public interface ITokenService
    {
        /// <summary>
        /// Will Return an account that will hold the token which is valid 7 days.
        /// </summary>
        /// <param name="account">Account of which you know username and password is correct please use, <see cref="IEncryptionService"/></param>
        /// <returns><see cref="Account"/> without password with token</returns>
        public Account Authenticate(Account account);
    }
}
