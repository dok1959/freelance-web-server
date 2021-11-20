using Microsoft.Extensions.Configuration;

namespace FreelanceWebServer.Services.JWT
{
    public class RefreshTokenGenerator
    {
        private IConfiguration _configuration;
        private TokenGenerator _tokenGenerator;

        public RefreshTokenGenerator(IConfiguration configuration, TokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        public string Generate()
        {
            var refreshTokenConfig = _configuration.GetSection("Authentication");

            var token = _tokenGenerator.Generate(
                refreshTokenConfig["RefreshTokenSecret"],
                refreshTokenConfig["Issuer"],
                refreshTokenConfig["Audience"],
                double.Parse(refreshTokenConfig["RefreshTokenExpirationMinutes"]));

            return token;
        }
    }
}
