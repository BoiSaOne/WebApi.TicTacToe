using WebApi.TicTacToe.Models;

namespace WebApi.TicTacToe.Repository
{
    public interface IRoomRepository
    {
        Task<bool> IsCreateGameAsync(string userName);
        Task<Room?> CreatedRoomAsync(string userName);
        Task<bool> DeleteRoomAsync(string userName);
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<bool> IsAvailableRoomAsync(string roomName, GameRules gameRules);
        Task<Room?> JoinRoomAsync(string roomName, string userName);
        Task<bool> IsRunGame(Room room, GameRules gameRules);
    }
}
