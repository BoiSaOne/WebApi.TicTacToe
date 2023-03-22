using WebApi.TicTacToe.Models;

namespace WebApi.TicTacToe.Hubs
{
    public interface IGameHub
    {
        Task NotifyGroupAsync(GameDTO? gameDTO);
        Task NotifyGroupPlayerMadeMoveAsync(GameDTO? gameDTO);
        Task NotifyResultGame(IEnumerable<ResultGamePlayer> resultGamePlayers);
    }
}