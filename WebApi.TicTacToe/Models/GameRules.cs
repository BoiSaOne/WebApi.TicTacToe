namespace WebApi.TicTacToe.Models
{
    /// <summary>
    /// Правила игры
    /// </summary>
    public class GameRules
    {
        /// <summary>
        /// Количество строк
        /// </summary>
        public readonly int RowsCount = 3;
        /// <summary>
        /// Количество колонок
        /// </summary>
        public readonly int ColumnsCount = 3;
        /// <summary>
        /// Количество игроков
        /// </summary>
        public readonly int NumberOfPlayers = 2;

        public GameRules(int RowsCount, int ColumnsCount, int NumberOfPlayers)
        {
            this.RowsCount = RowsCount <= 0 ? this.RowsCount : RowsCount;
            this.ColumnsCount = ColumnsCount <= 0 ? this.ColumnsCount : ColumnsCount;
            this.NumberOfPlayers = NumberOfPlayers <= 1 ? this.NumberOfPlayers : NumberOfPlayers;
        }

        public GameRules() { }
    }
}
