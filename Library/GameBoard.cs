namespace Library;

public enum GameStatus
{
    SymbolSelection,
    Active,
    PlayerWon,
    ComputerWon,
    Draw
}

public class GameBoard
{
    public char[] Cells { get; private set; } = new char[9];
    public char PlayerSymbol { get; set; }
    public char ComputerSymbol { get; set; }
    public GameStatus Status { get; set; } = GameStatus.SymbolSelection;
    public bool IsPlayerTurn { get; set; }

    public void Reset()
    {
        Cells = new char[9];
        PlayerSymbol = '\0';
        ComputerSymbol = '\0';
        Status = GameStatus.SymbolSelection;
        IsPlayerTurn = false;
    }
}
