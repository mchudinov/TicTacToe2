using Library;
using Xunit;

namespace Tests;

public class GameBoardTests
{
    [Fact]
    public void NewGameBoard_HasSymbolSelectionStatus_AndAllCellsEmpty()
    {
        var board = new GameBoard();

        Assert.Equal(GameStatus.SymbolSelection, board.Status);
        Assert.Equal(9, board.Cells.Length);
        Assert.All(board.Cells, cell => Assert.Equal('\0', cell));
    }
}
