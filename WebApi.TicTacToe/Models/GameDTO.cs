namespace WebApi.TicTacToe.Models
{
    public class GameDTO
    {
        public Game Game { get; set; } = null!;
        public string PlayerMakeMove { get; set; } = null!;
        public string Room { get; set; } = null!;
        public IEnumerable<string> QueuePlayers { get; set; } = null!;
        public int NumberOfMoves { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public GameDTO(Game game, Room room, User playerMakeMove, IEnumerable<User> players)
        {
            Room = room.NameRoom;
            PlayerMakeMove = playerMakeMove.Name;
            QueuePlayers = players.Select(p => p.Name);
            Game = game;
        }

        public GameDTO() { }
    }
}
