using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Services.JWT
{
    public class AccessTokenGenerator
    {
        private IConfiguration _configuration;
        private TokenGenerator _tokenGenerator;

        public AccessTokenGenerator(IConfiguration configuration, TokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        public string Generate(User user, string roleName)
        {
            var accessTokenConfig = _configuration.GetSection("Authentication");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)
            };

            var token = _tokenGenerator.Generate(
                accessTokenConfig["AccessTokenSecret"],
                accessTokenConfig["Issuer"],
                accessTokenConfig["Audience"],
                double.Parse(accessTokenConfig["AccessTokenExpirationMinutes"]),
                claims);

            return token;
        }
    }
}
