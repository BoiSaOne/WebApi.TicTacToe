using WebApi.TicTacToe.Models;

namespace WebApi.TicTacToe.Repository
{
    public interface IAuthRepository
    {
        Task<User> Register(string login, string password);
        Task<User?> Login(string userName, string password);
        Task<bool> IsUserExists(string username);
    }
}
