using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.TicTacToe.Models;
using WebApi.TicTacToe.Repository;

namespace WebApi.TicTacToe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly GameRules _gameRules = new GameRules();

        public RoomsController(IRoomRepository repository)
        {
            _roomRepository = repository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRoom(string userName)
        {
            if (await _roomRepository.IsCreateGameAsync(userName))
            {
                return BadRequest(new ModelResultDTO("Игра для пользователя уже создана"));
            }

            var room = await _roomRepository.CreatedRoomAsync(userName);
            return Ok(room);
        }

        [Authorize]
        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteRoom(string userName)
        {
            if (!await _roomRepository.IsCreateGameAsync(userName))
            {
                return BadRequest(new ModelResultDTO($"У игрока {userName} нет комнат"));
            }

            if (!await _roomRepository.DeleteRoomAsync(userName))
            {
                return BadRequest(new ModelResultDTO($"Ошибка удаления комнаты для пользователя {userName}"));
            }

            return Ok(new ModelResultDTO($"Комната пользователя {userName} удалена"));
        }

        [Authorize]
        [HttpGet("{roomName}")]
        public async Task<IActionResult> JoinRoom(string roomName, string userName)
        {
            if (!await _roomRepository.IsAvailableRoomAsync(roomName, _gameRules))
            {
                return BadRequest(new ModelResultDTO($"Комната '{roomName}' уже не доступна"));
            }

            var room = await _roomRepository.JoinRoomAsync(roomName, userName);
            if (room == null)
            {
                return BadRequest(new ModelResultDTO("Ошибка подключения к комнате"));
            }

            if (await _roomRepository.IsRunGame(room, _gameRules))
            {
                return RedirectToAction("GetGame", "Game", new { groupName = room.NameRoom });
            }

            return Ok(new ModelResultDTO($"Пользователь {userName} добавлен в комнату {roomName}"));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllRoomsWaiting()
        {
            var listRooms = await _roomRepository.GetAllRoomsAsync();
            return Ok(listRooms);
        }

    }
}
