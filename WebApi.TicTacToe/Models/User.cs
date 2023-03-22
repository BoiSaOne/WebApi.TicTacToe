using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.TicTacToe.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
        /// <summary>
        /// Хэш пароля
        /// </summary>
        [JsonIgnore]
        public byte[] PasswordHash { get; set; } = null!;
        /// <summary>
        /// Соль для хэша
        /// </summary>
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; } = null!;
        /// <summary>
        /// Комната в которой игрок является хостом
        /// </summary>
        [JsonIgnore]
        public Room? Room { get; set; }
        /// <summary>
        /// Комната в которой находится игрок
        /// </summary>
        [JsonIgnore]
        public RoomPlayers? RoomPlayer { get; set; }
        /// <summary>
        /// Список ходов, которые сделал игрок
        /// </summary>
        [JsonIgnore]
        public IEnumerable<PlayersMakeMove>? PlayerMakeMoves { get; set; }
        /// <summary>
        /// Список результатов игр
        /// </summary>
        [JsonIgnore]
        public IEnumerable<ResultGamePlayer>? ResultGamePlayer { get; set; }

        public User(string name)
        {
            Name = name;
        }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public User() { }
    }
}
