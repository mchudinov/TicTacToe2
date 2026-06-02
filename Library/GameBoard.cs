namespace Library
{
    /// <summary>
    /// Pure state container for a Tic-Tac-Toe game. Holds no game logic.
    /// </summary>
    public class GameBoard
    {
        public char[] Cells { get; } = new char[9];
        public char PlayerSymbol { get; set; }
        public char ComputerSymbol { get; set; }
        public GameStatus Status { get; set; }
        public bool IsPlayerTurn { get; set; }

        public GameBoard()
        {
            Reset();
        }

        /// <summary>
        /// Returns the board to its initial <see cref="GameStatus.SymbolSelection"/> state.
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = '\0';
            }
            PlayerSymbol = '\0';
            ComputerSymbol = '\0';
            Status = GameStatus.SymbolSelection;
            IsPlayerTurn = false;
        }
    }
}
