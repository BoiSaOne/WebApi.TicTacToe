using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.TicTacToe.Models
{
    /// <summary>
    /// Комната для игры
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Id комнаты
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя комнаты
        /// </summary>
        public string NameRoom { get; set; } = null!;
        /// <summary>
        /// Хост комнаты
        /// </summary>
        [JsonIgnore]
        public User? Host { get; set; }
        /// <summary>
        /// Id хоста
        /// </summary>
        [JsonIgnore]
        public int? HostId { get; set; }
        /// <summary>
        /// Статус комнаты
        /// </summary>
        [Required]
        public StatusGame StatusRoom { get; set; } = StatusGame.Waiting;
        /// <summary>
        /// Список игроков
        /// </summary>
        [JsonIgnore]
        public ICollection<RoomPlayers>? RoomPlayers { get; set; }

        public Room(string nameRoom, User host)
        {
            NameRoom = nameRoom;
            Host = host;
        }

        public Room() { }

        public Room(int id, string nameRoom, StatusGame statusGame)
        {
            Id = id;
            NameRoom = nameRoom;
            StatusRoom = statusGame;
        }
    }

    public enum StatusGame
    {
        Waiting,
        InGame
    }
}
