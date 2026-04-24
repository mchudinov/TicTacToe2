using Library;
using Xunit;

namespace Tests;

public class ComputerPlayerTests
{
    // ── Always returns an empty cell index ───────────────────────────────────

    [Fact]
    public void PickCell_ReturnsEmptyCellIndex_WhenMultipleCellsEmpty()
    {
        var cells = new char[9]; // all empty

        var index = ComputerPlayer.PickCell(cells);

        Assert.Equal('\0', cells[index]);
    }

    [Fact]
    public void PickCell_ReturnsEmptyCellIndex_WhenSomeCellsOccupied()
    {
        var cells = new char[]
        {
            'X', 'O', 'X',
            '\0', 'X', '\0',
            'O', '\0', 'O'
        };

        var index = ComputerPlayer.PickCell(cells);

        Assert.Equal('\0', cells[index]);
    }

    // ── Never returns an occupied cell index ─────────────────────────────────

    [Fact]
    public void PickCell_NeverReturnsOccupiedCellIndex()
    {
        // Run many times to confirm randomness never picks an occupied cell
        var cells = new char[]
        {
            'X', '\0', 'O',
            '\0', 'X', '\0',
            'O', '\0', 'X'
        };

        for (int i = 0; i < 200; i++)
        {
            var index = ComputerPlayer.PickCell(cells);
            Assert.Equal('\0', cells[index]);
        }
    }

    // ── Full board throws ────────────────────────────────────────────────────

    [Fact]
    public void PickCell_ThrowsInvalidOperationException_WhenBoardIsFull()
    {
        var cells = new char[]
        {
            'X', 'O', 'X',
            'O', 'X', 'O',
            'X', 'O', 'X'
        };

        Assert.Throws<InvalidOperationException>(() => ComputerPlayer.PickCell(cells));
    }

    // ── Exactly one empty cell remaining ─────────────────────────────────────

    [Fact]
    public void PickCell_ReturnsOnlyEmptyCellIndex_WhenOneCellRemains()
    {
        var cells = new char[]
        {
            'X', 'O', 'X',
            'O', 'X', 'O',
            'O', 'X', '\0'  // only index 8 is empty
        };

        var index = ComputerPlayer.PickCell(cells);

        Assert.Equal(8, index);
    }
}
