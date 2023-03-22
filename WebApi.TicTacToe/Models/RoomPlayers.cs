using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.TicTacToe.Models
{
    /// <summary>
    /// Игроки комнаты
    /// </summary>
    public class RoomPlayers
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Комната
        /// </summary>
        public Room? Room { get; set; }
        /// <summary>
        /// Id команаты
        /// </summary>
        public int? RoomId { get; set; }
        /// <summary>
        /// Игрок
        /// </summary>
        public User? Player { get; set; }
        /// <summary>
        /// Id игрока
        /// </summary>
        public int? PlayerId { get; set; }

        public RoomPlayers(Room? room, User? player)
        {
            Room = room;
            Player = player;
        }

        public RoomPlayers() { }
    }
}
