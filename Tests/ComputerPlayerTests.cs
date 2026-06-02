using Library;
using Xunit;

namespace Tests
{
    public class ComputerPlayerTests
    {
        private static char[] EmptyBoard() => new char[9]
        {
            '\0', '\0', '\0',
            '\0', '\0', '\0',
            '\0', '\0', '\0'
        };

        [Fact]
        public void PickCell_OnEmptyBoard_ReturnsIndexInRange()
        {
            var cells = EmptyBoard();

            for (int i = 0; i < 100; i++)
            {
                int pick = ComputerPlayer.PickCell(cells);

                Assert.InRange(pick, 0, 8);
                Assert.Equal('\0', cells[pick]);
            }
        }

        [Fact]
        public void PickCell_NeverReturnsAnOccupiedIndex()
        {
            // Only indices 2 and 7 are empty; everything else is taken.
            var cells = new char[9]
            {
                'X', 'O', '\0',
                'X', 'O', 'X',
                'O', '\0', 'X'
            };

            for (int i = 0; i < 200; i++)
            {
                int pick = ComputerPlayer.PickCell(cells);

                Assert.Equal('\0', cells[pick]);
                Assert.Contains(pick, new[] { 2, 7 });
            }
        }

        [Fact]
        public void PickCell_WithOnlyOneEmptyCell_ReturnsThatIndex()
        {
            // Only index 4 is empty.
            var cells = new char[9]
            {
                'X', 'O', 'X',
                'O', '\0', 'X',
                'O', 'X', 'O'
            };

            for (int i = 0; i < 50; i++)
            {
                int pick = ComputerPlayer.PickCell(cells);

                Assert.Equal(4, pick);
            }
        }

        [Fact]
        public void PickCell_WithOnlyFirstCellEmpty_ReturnsZero()
        {
            var cells = new char[9]
            {
                '\0', 'O', 'X',
                'O', 'X', 'X',
                'O', 'X', 'O'
            };

            int pick = ComputerPlayer.PickCell(cells);

            Assert.Equal(0, pick);
        }

        [Fact]
        public void PickCell_WithOnlyLastCellEmpty_ReturnsEight()
        {
            var cells = new char[9]
            {
                'X', 'O', 'X',
                'O', 'X', 'X',
                'O', 'X', '\0'
            };

            int pick = ComputerPlayer.PickCell(cells);

            Assert.Equal(8, pick);
        }
    }
}
