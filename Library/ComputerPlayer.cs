namespace Library
{
    /// <summary>
    /// Computer move selection for a Tic-Tac-Toe board. Stateless.
    /// </summary>
    public static class ComputerPlayer
    {
        /// <summary>
        /// Collects all empty cell indices from <paramref name="cells"/> and returns one at random.
        /// </summary>
        /// <param name="cells">The board cells. Empty cells are represented by <c>'\0'</c>.</param>
        /// <returns>The index of a randomly chosen empty cell.</returns>
        public static int PickCell(char[] cells)
        {
            var empty = new List<int>(cells.Length);
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] == '\0')
                {
                    empty.Add(i);
                }
            }

            return empty[Random.Shared.Next(empty.Count)];
        }
    }
}
