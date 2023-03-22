using System.ComponentModel.DataAnnotations;

namespace WebApi.TicTacToe.Models
{
    /// <summary>
    /// Список ходов игроков
    /// </summary>
    public class PlayersMakeMove
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Игра
        /// </summary>
        public Game? Game { get; set; }
        /// <summary>
        /// Id игры
        /// </summary>
        public int GameId { get; set; }
        /// <summary>
        /// Игрок сделавший ход
        /// </summary>
        public User? Player { get; set; }
        /// <summary>
        /// Id игрока
        /// </summary>
        public int PlayerId { get; set; }
        /// <summary>
        /// Колонка
        /// </summary>
        [Required]
        public int Column { get; set; }
        /// <summary>
        /// Строка
        /// </summary>
        [Required]
        public int Row { get; set; }

        public PlayersMakeMove(Game game, User user, int column, int row)
        {
            Game = game;
            Player = user;
            Column = column;
            Row = row;
        }

        public PlayersMakeMove() { }
    }
}
