using System.Text.Json.Serialization;

namespace WebApi.TicTacToe.Models
{
    /// <summary>
    /// Игра
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Id игры
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Дата создания игры
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;
        /// <summary>
        /// Список ходов игроков
        /// </summary>
        [JsonIgnore]
        public IEnumerable<PlayersMakeMove>? PlayersMakeMoves {get; set;}
        /// <summary>
        /// Результат игры
        /// </summary>
        [JsonIgnore]
        public IEnumerable<ResultGamePlayer>? ResultGamePlayers { get; set; }
    }
}
