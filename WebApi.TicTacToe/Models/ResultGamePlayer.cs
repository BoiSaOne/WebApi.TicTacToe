namespace WebApi.TicTacToe.Models
{
    /// <summary>
    /// Результат игры
    /// </summary>
    public class ResultGamePlayer
    {
        /// <summary>
        /// Id 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Игрок
        /// </summary>
        public User? Player { get; set; }
        /// <summary>
        /// Id игрока
        /// </summary>
        public int PlayerId { get; set; }
        /// <summary>
        /// Игра
        /// </summary>
        public Game? Game { get; set; }
        /// <summary>
        /// Id игры
        /// </summary>
        public int GameId { get; set; }
        /// <summary>
        /// Результат игры для игрока
        /// </summary>
        public ResultGame ResultGame { get; set; }

        public ResultGamePlayer(User? player, Game? game, ResultGame resultGame)
        {
            Player = player;
            Game = game;
            ResultGame = resultGame;
        }

        public ResultGamePlayer() { }

    }

    public enum ResultGame
    {
        Victory,
        Draw,
        Loss
    }
}
