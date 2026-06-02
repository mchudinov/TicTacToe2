namespace Library
{
    /// <summary>
    /// Pure rule evaluation for a Tic-Tac-Toe board. Stateless.
    /// </summary>
    public static class GameRules
    {
        private static readonly int[][] WinLines = new[]
        {
            // Rows
            new[] { 0, 1, 2 },
            new[] { 3, 4, 5 },
            new[] { 6, 7, 8 },
            // Columns
            new[] { 0, 3, 6 },
            new[] { 1, 4, 7 },
            new[] { 2, 5, 8 },
            // Diagonals
            new[] { 0, 4, 8 },
            new[] { 2, 4, 6 }
        };

        /// <summary>
        /// Evaluates the current board and returns the corresponding <see cref="GameStatus"/>:
        /// <see cref="GameStatus.PlayerWon"/>, <see cref="GameStatus.ComputerWon"/>,
        /// <see cref="GameStatus.Draw"/>, or <see cref="GameStatus.Active"/>.
        /// </summary>
        public static GameStatus CheckResult(char[] cells, char playerSymbol, char computerSymbol)
        {
            foreach (var line in WinLines)
            {
                char a = cells[line[0]];
                if (a == '\0') continue;

                if (a == cells[line[1]] && a == cells[line[2]])
                {
                    if (a == playerSymbol) return GameStatus.PlayerWon;
                    if (a == computerSymbol) return GameStatus.ComputerWon;
                }
            }

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] == '\0') return GameStatus.Active;
            }

            return GameStatus.Draw;
        }
    }
}
