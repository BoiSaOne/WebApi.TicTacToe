using System.ComponentModel.DataAnnotations;

namespace WebApi.TicTacToe.Models
{
    public class UserRegisterDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = $"{nameof(Login)} долежн содержать минимум 5 символов")]
        public string Login { get; set; } = null!;
        [Required]
        [MinLength(5, ErrorMessage = $"{nameof(Password)} долежн содержать минимум 5 символов")]
        public string Password { get; set; } = null!;
    }
}
