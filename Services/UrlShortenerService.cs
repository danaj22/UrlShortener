using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using UrlShortener.Entities;

namespace UrlShortener.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly IConfiguration _configuration;
        public UrlShortenerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateCode()
        {
            var signs = _configuration["Settings:AvaliableSigns"];
            int.TryParse(_configuration["Settings:CodeLength"], out int codeLength);

            var codeGenerated = RandomNumberGenerator
                .GetItems(signs.AsSpan(), codeLength);

            var code = new string(codeGenerated);
            return code;
        }
    }
}
