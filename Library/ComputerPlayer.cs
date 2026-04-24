namespace Library;

public static class ComputerPlayer
{
    private static readonly Random _random = new();

    /// <summary>
    /// Picks a random empty cell index from the board.
    /// </summary>
    /// <param name="cells">The 9-element board array; '\0' means empty.</param>
    /// <returns>Index of a randomly chosen empty cell.</returns>
    public static int PickCell(char[] cells)
    {
        var emptyCells = new List<int>();
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] == '\0')
                emptyCells.Add(i);
        }

        return emptyCells[_random.Next(emptyCells.Count)];
    }
}
