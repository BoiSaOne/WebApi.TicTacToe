using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebApi.TicTacToe.Database;
using WebApi.TicTacToe.Models;

namespace WebApi.TicTacToe.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationContext db;

        public AuthRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<User?> Login(string userName, string password)
        {
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == userName);
            if (user != null && VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return user;
            }
            return null;
        }

        public async Task<User> Register(string login, string password)
        {
            User user = new User(login);
            (user.PasswordHash, user.PasswordSalt) = CreatePasswordHash(password);
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> IsUserExists(string username) =>
            await db.Users.AnyAsync(n => n.Name == username);

        private (byte[] PasswordHash, byte[] PasswordSalt) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return (hmac.ComputeHash(new UTF8Encoding().GetBytes(password)), hmac.Key);
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
