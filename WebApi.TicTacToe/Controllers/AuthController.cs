using Microsoft.AspNetCore.Mvc;
using WebApi.TicTacToe.Models;
using WebApi.TicTacToe.Repository;

namespace WebApi.TicTacToe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            if (await _authRepository.IsUserExists(request.Login))
            {
                return BadRequest(new ModelResultDTO("Пользователь уже зарегистрирован"));
            }

            User user = await _authRepository.Register(request.Login, request.Password);
            return Ok(new { user = user, accessToken = TokenGenerator.GetToken(request.Login) });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserRegisterDTO request)
        {
            var user = await _authRepository.Login(request.Login, request.Password);
            if (user == null)
            {
                return BadRequest(new ModelResultDTO("Не верно ввели логин или пароль"));
            }

            return Ok(new { user = user, accessToken = TokenGenerator.GetToken(request.Login) });
        }
    }
}
