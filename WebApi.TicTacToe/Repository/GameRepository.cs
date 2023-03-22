using Microsoft.EntityFrameworkCore;
using System;
using WebApi.TicTacToe.Database;
using WebApi.TicTacToe.Models;

namespace WebApi.TicTacToe.Repository
{
    public class GameRepository : IGameRepository
    {

        private readonly ApplicationContext db;
        private readonly GameRules _gameRules = new GameRules();

        public GameRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddMakeMoveAsync(GameDTO gameDTO)
        {
            var game = await db.Games.FirstOrDefaultAsync(g => g.Id == gameDTO.Game.Id);
            var user = await db.Users.FirstOrDefaultAsync(u => u.Name == gameDTO.PlayerMakeMove);
            if (game == null || user == null)
            {
                return false;
            }

            PlayersMakeMove playersMakeMove = new PlayersMakeMove(game, user, gameDTO.Column, gameDTO.Row);
            await db.PlayersMakeMoves.AddAsync(playersMakeMove);
            await db.SaveChangesAsync();

            return true;
        }

        public GameDTO GetGameDTO(GameDTO gameDTO)
        {
            gameDTO.PlayerMakeMove = GetNextPlayerFromQueue(gameDTO.PlayerMakeMove, gameDTO.QueuePlayers);
            return gameDTO;
        }

        private string GetNextPlayerFromQueue(string playerMakeMove, IEnumerable<string> queuePlayers)
        {
            string? userFind = queuePlayers.SkipWhile(n => n != playerMakeMove).Skip(1).FirstOrDefault();
            return userFind ?? queuePlayers?.FirstOrDefault() ?? playerMakeMove;
        }

        public async Task<Game> CreateGameAsync()
        {
            Game newGame = new Game();
            db.Games.Add(newGame);
            await db.SaveChangesAsync();
            return newGame;
        }

        public async Task<GameDTO?> GetGameDTOAsync(string roomName, Game game)
        {
            var room = await db.Rooms.FirstOrDefaultAsync(r => r.NameRoom == roomName);
            if (room == null)
            {
                return null;
            }

            var listPlayersRoom = await db.RoomPlayers.Where(r => r.Room == room && r.Player != null).Select(r => r.Player).ToListAsync();
            if (listPlayersRoom?.Count == 0)
            {
                return null;
            }

            return new GameDTO(game, room, listPlayersRoom[0], listPlayersRoom);

        }

        public async Task<bool> IsMakeMoveAsync(Game game, int column, int row)
        {
            var gameDb = await db.Games.FirstOrDefaultAsync(g => g.Id == game.Id);
            if (gameDb == null)
            {
                return false;
            }

            return await db.PlayersMakeMoves.AnyAsync(p => p.Game == gameDb && p.Column == column && p.Row == row);
        }

        public async Task<bool> СheckingGameConditionsAsync(Game game, string userName)
        {
            var gameDb = await db.Games.FirstOrDefaultAsync(g => g.Id == game.Id);
            var user = await db.Users.FirstOrDefaultAsync(u => u.Name == userName);
            if (gameDb == null || user == null)
            {
                return false;
            }

            var array = await GetArrayPlayerMakeMovesAsync(gameDb, user);
            return CheckDiagonal(array) || CheckLanes(array);

        }

        private async Task<bool[,]> GetArrayPlayerMakeMovesAsync(Game game, User player)
        {
            var playerMakeMoves = await db.PlayersMakeMoves.Where(p => p.Player == player && p.Game == game).ToListAsync();
            bool[,] arr = new bool[_gameRules.ColumnsCount, _gameRules.RowsCount];
            for (int i = 0; i < _gameRules.ColumnsCount; i++)
            {
                for (int j = 0; j < _gameRules.RowsCount; j++)
                {
                    arr[i, j] = playerMakeMoves.Where(p => p.Row-1 == j && p.Column-1 == i).Any();
                }
            }

            return arr;
        }

        private bool CheckDiagonal(bool[,] arr)
        {
            bool toright = true;
            bool toleft = true;
            for (int i = 0; i < _gameRules.ColumnsCount; i++)
            {
                toright &= arr[i, i];
                toleft &= arr[_gameRules.ColumnsCount - i - 1, i];
            }

            if (toright || toleft) return true;

            return false;

        }

        private bool CheckLanes(bool[,] arr)
        {
            bool cols = true;
            bool rows = true;

            for (int col = 0; col < _gameRules.ColumnsCount; col++)
            {
                cols = true;
                rows = true;
                for (int row = 0; row < _gameRules.RowsCount; row++)
                {
                    cols &= arr[col, row];
                    rows &= arr[row, col];
                }

                if (cols || rows) return true;
            }

            return false;
        }

        public async Task<IEnumerable<ResultGamePlayer>> GetResultGame(GameDTO game, string? userWinName)
        {
            var gameDb = await db.Games.FirstOrDefaultAsync(g => g.Id == game.Game.Id);
            var resultGames = new List<ResultGamePlayer>();
            foreach (var userName in game.QueuePlayers)
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Name == userName);
                ResultGame resultGame = userWinName == null ? ResultGame.Draw :
                    userWinName == user?.Name ? ResultGame.Victory : ResultGame.Loss;

                var result = new ResultGamePlayer(user, gameDb, resultGame);
                resultGames.Add(result);
            }

            var room = await db.Rooms.FirstOrDefaultAsync(r => r.NameRoom == game.Room);
            if (room != null)
            {
                var roomPlayers = await db.RoomPlayers.Where(r => r.Room == room).ToListAsync();
                db.Rooms.Remove(room);
                db.RoomPlayers.RemoveRange(roomPlayers);
                db.ResultGamePlayers.AddRange(resultGames);
                await db.SaveChangesAsync();
            }

            return resultGames;
             
        }
    }
}
