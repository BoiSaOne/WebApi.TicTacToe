using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.TicTacToe.Models
{
    public static class AuthOptions
    {
        public const string ISSUER = "Boikov A.S.";
        public const string AUDIENCE = "WebApi.TicTacToe";
        private const string KEY = "mysupersecret_secretkey!123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
