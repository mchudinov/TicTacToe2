namespace Library;

public static class GameRules
{
    private static readonly int[][] WinLines =
    [
        [0, 1, 2], // row 0
        [3, 4, 5], // row 1
        [6, 7, 8], // row 2
        [0, 3, 6], // col 0
        [1, 4, 7], // col 1
        [2, 5, 8], // col 2
        [0, 4, 8], // diagonal \
        [2, 4, 6], // diagonal /
    ];

    public static GameStatus CheckResult(char[] cells, char playerSymbol, char computerSymbol)
    {
        foreach (var line in WinLines)
        {
            if (cells[line[0]] == playerSymbol && cells[line[1]] == playerSymbol && cells[line[2]] == playerSymbol)
                return GameStatus.PlayerWon;

            if (cells[line[0]] == computerSymbol && cells[line[1]] == computerSymbol && cells[line[2]] == computerSymbol)
                return GameStatus.ComputerWon;
        }

        // No winner — check for draw (all cells filled)
        foreach (var cell in cells)
        {
            if (cell == '\0')
                return GameStatus.Active;
        }

        return GameStatus.Draw;
    }
}
