using Library;
using Xunit;

namespace Tests
{
    public class GameBoardTests
    {
        [Fact]
        public void NewBoard_IsInInitialSymbolSelectionState()
        {
            var board = new GameBoard();

            Assert.Equal(9, board.Cells.Length);
            Assert.All(board.Cells, c => Assert.Equal('\0', c));
            Assert.Equal('\0', board.PlayerSymbol);
            Assert.Equal('\0', board.ComputerSymbol);
            Assert.Equal(GameStatus.SymbolSelection, board.Status);
            Assert.False(board.IsPlayerTurn);
        }

        [Fact]
        public void Reset_RestoresInitialState()
        {
            var board = new GameBoard
            {
                PlayerSymbol = 'X',
                ComputerSymbol = 'O',
                Status = GameStatus.Active,
                IsPlayerTurn = true
            };
            board.Cells[0] = 'X';
            board.Cells[4] = 'O';
            board.Cells[8] = 'X';

            board.Reset();

            Assert.All(board.Cells, c => Assert.Equal('\0', c));
            Assert.Equal('\0', board.PlayerSymbol);
            Assert.Equal('\0', board.ComputerSymbol);
            Assert.Equal(GameStatus.SymbolSelection, board.Status);
            Assert.False(board.IsPlayerTurn);
        }
    }
}
