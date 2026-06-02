using Library;
using Xunit;

namespace Tests
{
    public class GameRulesTests
    {
        private static char[] EmptyBoard() => new char[9]
        {
            '\0', '\0', '\0',
            '\0', '\0', '\0',
            '\0', '\0', '\0'
        };

        private static char[] BoardWithLine(int a, int b, int c, char symbol)
        {
            var cells = EmptyBoard();
            cells[a] = symbol;
            cells[b] = symbol;
            cells[c] = symbol;
            return cells;
        }

        // ---------------- Rows ----------------

        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(3, 4, 5)]
        [InlineData(6, 7, 8)]
        public void PlayerWins_OnEachRow(int a, int b, int c)
        {
            var cells = BoardWithLine(a, b, c, 'X');

            var result = GameRules.CheckResult(cells, playerSymbol: 'X', computerSymbol: 'O');

            Assert.Equal(GameStatus.PlayerWon, result);
        }

        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(3, 4, 5)]
        [InlineData(6, 7, 8)]
        public void ComputerWins_OnEachRow(int a, int b, int c)
        {
            var cells = BoardWithLine(a, b, c, 'O');

            var result = GameRules.CheckResult(cells, playerSymbol: 'X', computerSymbol: 'O');

            Assert.Equal(GameStatus.ComputerWon, result);
        }

        // ---------------- Columns ----------------

        [Theory]
        [InlineData(0, 3, 6)]
        [InlineData(1, 4, 7)]
        [InlineData(2, 5, 8)]
        public void PlayerWins_OnEachColumn(int a, int b, int c)
        {
            var cells = BoardWithLine(a, b, c, 'X');

            var result = GameRules.CheckResult(cells, playerSymbol: 'X', computerSymbol: 'O');

            Assert.Equal(GameStatus.PlayerWon, result);
        }

        [Theory]
        [InlineData(0, 3, 6)]
        [InlineData(1, 4, 7)]
        [InlineData(2, 5, 8)]
        public void ComputerWins_OnEachColumn(int a, int b, int c)
        {
            var cells = BoardWithLine(a, b, c, 'O');

            var result = GameRules.CheckResult(cells, playerSymbol: 'X', computerSymbol: 'O');

            Assert.Equal(GameStatus.ComputerWon, result);
        }

        // ---------------- Diagonals ----------------

        [Theory]
        [InlineData(0, 4, 8)]
        [InlineData(2, 4, 6)]
        public void PlayerWins_OnEachDiagonal(int a, int b, int c)
        {
            var cells = BoardWithLine(a, b, c, 'X');

            var result = GameRules.CheckResult(cells, playerSymbol: 'X', computerSymbol: 'O');

            Assert.Equal(GameStatus.PlayerWon, result);
        }

        [Theory]
        [InlineData(0, 4, 8)]
        [InlineData(2, 4, 6)]
        public void ComputerWins_OnEachDiagonal(int a, int b, int c)
        {
            var cells = BoardWithLine(a, b, c, 'O');

            var result = GameRules.CheckResult(cells, playerSymbol: 'X', computerSymbol: 'O');

            Assert.Equal(GameStatus.ComputerWon, result);
        }

        // ---------------- Draw ----------------

        [Fact]
        public void FullBoardWithNoLine_IsDraw()
        {
            // X O X
            // X O O
            // O X X
            var cells = new char[9]
            {
                'X', 'O', 'X',
                'X', 'O', 'O',
                'O', 'X', 'X'
            };

            var result = GameRules.CheckResult(cells, playerSymbol: 'X', computerSymbol: 'O');

            Assert.Equal(GameStatus.Draw, result);
        }

        // ---------------- Active ----------------

        [Fact]
        public void InProgressBoard_IsActive()
        {
            // X . .
            // . O .
            // . . .
            var cells = EmptyBoard();
            cells[0] = 'X';
            cells[4] = 'O';

            var result = GameRules.CheckResult(cells, playerSymbol: 'X', computerSymbol: 'O');

            Assert.Equal(GameStatus.Active, result);
        }

        [Fact]
        public void EmptyBoard_IsActive()
        {
            var cells = EmptyBoard();

            var result = GameRules.CheckResult(cells, playerSymbol: 'X', computerSymbol: 'O');

            Assert.Equal(GameStatus.Active, result);
        }

        // ---------------- Symbol assignment is honored ----------------

        [Fact]
        public void PlayerSymbolO_WinningLine_ReportsPlayerWon()
        {
            // Player chose 'O', computer is 'X'. A row of 'O' should be PlayerWon.
            var cells = BoardWithLine(0, 1, 2, 'O');

            var result = GameRules.CheckResult(cells, playerSymbol: 'O', computerSymbol: 'X');

            Assert.Equal(GameStatus.PlayerWon, result);
        }

        [Fact]
        public void ComputerSymbolX_WinningLine_ReportsComputerWon()
        {
            // Player chose 'O', computer is 'X'. A row of 'X' should be ComputerWon.
            var cells = BoardWithLine(0, 1, 2, 'X');

            var result = GameRules.CheckResult(cells, playerSymbol: 'O', computerSymbol: 'X');

            Assert.Equal(GameStatus.ComputerWon, result);
        }
    }
}
