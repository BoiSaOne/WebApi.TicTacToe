using Microsoft.EntityFrameworkCore;
using WebApi.TicTacToe.Database;
using WebApi.TicTacToe.Models;

namespace WebApi.TicTacToe.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationContext db;

        public RoomRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<Room?> CreatedRoomAsync(string userName)
        {
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == userName);
            if (user == null)
            {
                return null;
            }

            var room = new Room(userName, user);
            db.RoomPlayers.Add(new RoomPlayers(room, user));
            await db.SaveChangesAsync();
            return room;
        }

        public async Task<bool> DeleteRoomAsync(string userName)
        {
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == userName);
            if (user == null)
                return false;

            var roomUser = await db.Rooms.Where(r => r.Host == user).FirstOrDefaultAsync();
            if (roomUser != null)
            {
                var roomPlayers = db.RoomPlayers.Where(p => p.Room == roomUser).ToArray();
                db.RoomPlayers.RemoveRange(roomPlayers);
                db.Rooms.Remove(roomUser);
                await db.SaveChangesAsync();
            }

            return true;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await db.Rooms.Where(r => r.StatusRoom == StatusGame.Waiting).ToListAsync();
        }

        public async Task<bool> IsAvailableRoomAsync(string roomName, GameRules gameRules)
        {
            var room = await db.Rooms.FirstOrDefaultAsync(r => r.NameRoom == roomName);
            if (room == null)
            {
                return false;
            }

            int countPlayer = await db.RoomPlayers.CountAsync(r => r.Room == room);
            return countPlayer < gameRules.NumberOfPlayers;
        }

        public async Task<bool> IsCreateGameAsync(string userName)
        {
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == userName);
            if (user == null)
                return false;

            return await db.RoomPlayers.CountAsync(p => p.Player == user) != 0;
        }

        public async Task<bool> IsRunGame(Room room, GameRules gameRules)
        {
            bool createGame = await db.RoomPlayers.CountAsync(r => r.Room == room) == gameRules.NumberOfPlayers;
            if (createGame)
            {
                room.StatusRoom = StatusGame.InGame;
                await db.SaveChangesAsync();
            }

            return createGame;
        }

        public async Task<Room?> JoinRoomAsync(string roomName, string userName)
        {
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == userName);
            Room? room = await db.Rooms.FirstOrDefaultAsync(r => r.NameRoom == roomName);
            if (user == null || room == null)
            {
                return null;
            }
            await db.RoomPlayers.AddAsync(new RoomPlayers(room, user));
            await db.SaveChangesAsync();

            return room;
        }
    }
}
