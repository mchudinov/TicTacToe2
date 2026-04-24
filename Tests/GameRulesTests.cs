using Library;
using Xunit;

namespace Tests;

public class GameRulesTests
{
    private const char X = 'X';
    private const char O = 'O';

    // Helper: build a board with specific win-line cells filled for one symbol, rest empty
    private static char[] BoardWithWinLine(int a, int b, int c, char symbol)
    {
        var cells = new char[9];
        cells[a] = symbol;
        cells[b] = symbol;
        cells[c] = symbol;
        return cells;
    }

    // ── Player wins (X) ──────────────────────────────────────────────────────

    [Theory]
    [InlineData(0, 1, 2)] // row 0
    [InlineData(3, 4, 5)] // row 1
    [InlineData(6, 7, 8)] // row 2
    [InlineData(0, 3, 6)] // col 0
    [InlineData(1, 4, 7)] // col 1
    [InlineData(2, 5, 8)] // col 2
    [InlineData(0, 4, 8)] // diagonal \
    [InlineData(2, 4, 6)] // diagonal /
    public void CheckResult_PlayerWins_AllWinLines(int a, int b, int c)
    {
        var cells = BoardWithWinLine(a, b, c, X);
        var result = GameRules.CheckResult(cells, playerSymbol: X, computerSymbol: O);
        Assert.Equal(GameStatus.PlayerWon, result);
    }

    // ── Computer wins (O) ────────────────────────────────────────────────────

    [Theory]
    [InlineData(0, 1, 2)] // row 0
    [InlineData(3, 4, 5)] // row 1
    [InlineData(6, 7, 8)] // row 2
    [InlineData(0, 3, 6)] // col 0
    [InlineData(1, 4, 7)] // col 1
    [InlineData(2, 5, 8)] // col 2
    [InlineData(0, 4, 8)] // diagonal \
    [InlineData(2, 4, 6)] // diagonal /
    public void CheckResult_ComputerWins_AllWinLines(int a, int b, int c)
    {
        var cells = BoardWithWinLine(a, b, c, O);
        var result = GameRules.CheckResult(cells, playerSymbol: X, computerSymbol: O);
        Assert.Equal(GameStatus.ComputerWon, result);
    }

    // ── Draw ─────────────────────────────────────────────────────────────────

    [Fact]
    public void CheckResult_Draw_WhenBoardFullAndNoWinner()
    {
        // X O X
        // O X X
        // O X O  — full board, no three-in-a-row for either
        var cells = new char[]
        {
            X, O, X,
            O, X, X,
            O, X, O
        };

        var result = GameRules.CheckResult(cells, playerSymbol: X, computerSymbol: O);
        Assert.Equal(GameStatus.Draw, result);
    }

    // ── Active (in progress) ─────────────────────────────────────────────────

    [Fact]
    public void CheckResult_Active_WhenBoardPartiallyFilled()
    {
        // Only a few moves made, no winner yet
        var cells = new char[9];
        cells[0] = X;
        cells[4] = O;

        var result = GameRules.CheckResult(cells, playerSymbol: X, computerSymbol: O);
        Assert.Equal(GameStatus.Active, result);
    }

    [Fact]
    public void CheckResult_Active_WhenBoardIsEmpty()
    {
        var cells = new char[9];
        var result = GameRules.CheckResult(cells, playerSymbol: X, computerSymbol: O);
        Assert.Equal(GameStatus.Active, result);
    }
}
