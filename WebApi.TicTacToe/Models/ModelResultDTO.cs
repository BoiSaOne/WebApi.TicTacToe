namespace WebApi.TicTacToe.Models
{
    public class ModelResultDTO
    {
        public string Message { get; set; }

        public ModelResultDTO(string message)
        {
            Message = message;
        }
    }
}
