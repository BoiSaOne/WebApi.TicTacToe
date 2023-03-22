using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.TicTacToe.Hubs;
using WebApi.TicTacToe.Models;
using WebApi.TicTacToe.Repository;

namespace WebApi.TicTacToe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IHubContext<GameHub, IGameHub> _gameHub;
        private readonly IGameRepository _gameRepository;
        private readonly GameRules _gameRules = new GameRules();

        public GameController(IHubContext<GameHub, IGameHub> gameHub, IGameRepository gameRepository)
        {
            _gameHub = gameHub;
            _gameRepository = gameRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetGame(string groupName)
        {
            Game game = await _gameRepository.CreateGameAsync();

            var response = await _gameRepository.GetGameDTOAsync(groupName, game);
            if (response == null)
            {
                return BadRequest(new ModelResultDTO("Ошибка входных данных"));
            }

            await _gameHub.Clients.Group(groupName).NotifyGroupAsync(response);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MovePlayer(GameDTO gameDTO)
        {
            if (await _gameRepository.IsMakeMoveAsync(gameDTO.Game, gameDTO.Column, gameDTO.Row))
            {
                return BadRequest(new ModelResultDTO("Данный ход был уже сделан"));
            }

            if (!await _gameRepository.AddMakeMoveAsync(gameDTO))
            {
                return BadRequest("Ошибка создания хода игрока");
            }
        
            if ((_gameRules.ColumnsCount + _gameRules.RowsCount - 1 <= gameDTO.NumberOfMoves) &&
                await _gameRepository.СheckingGameConditionsAsync(gameDTO.Game, gameDTO.PlayerMakeMove) ||
                (_gameRules.ColumnsCount * _gameRules.RowsCount) == gameDTO.NumberOfMoves)
            {
                string? userName = (_gameRules.ColumnsCount * _gameRules.RowsCount) == gameDTO.NumberOfMoves ? null : gameDTO.PlayerMakeMove;
                var resultGame = await _gameRepository.GetResultGame(gameDTO, userName);
                await _gameHub.Clients.Group(gameDTO.Room).NotifyResultGame(resultGame);
            }

            gameDTO = _gameRepository.GetGameDTO(gameDTO);
            await _gameHub.Clients.Group(gameDTO.Room).NotifyGroupPlayerMadeMoveAsync(gameDTO);

            return Ok();
        }
    }
}
