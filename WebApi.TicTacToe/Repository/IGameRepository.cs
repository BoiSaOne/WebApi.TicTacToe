using WebApi.TicTacToe.Models;

namespace WebApi.TicTacToe.Repository
{
    public interface IGameRepository
    {
        Task<GameDTO?> GetGameDTOAsync(string roomName, Game game);
        Task<Game> CreateGameAsync();
        Task<bool> IsMakeMoveAsync(Game game, int column, int row);
        Task<bool> AddMakeMoveAsync(GameDTO gameDTO);
        Task<bool> СheckingGameConditionsAsync(Game game, string userName);
        GameDTO GetGameDTO(GameDTO gameDTO);
        Task<IEnumerable<ResultGamePlayer>> GetResultGame(GameDTO game, string? userNameWin);
    }
}
