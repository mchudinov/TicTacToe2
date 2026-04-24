# TicTacToe Game Implementation Design

**Date:** 2026-04-24
**Scope:** Single-player vs. computer, as defined in `docs/poc.md`

---

## Architecture

Three new classes added to `Library/`:

| Class | Responsibility |
|---|---|
| `GameBoard` | Mutable state container — cells, symbols, status, whose turn |
| `GameRules` | Stateless win/draw detection across the 8 win lines |
| `ComputerPlayer` | Picks a random empty cell index |

`Home.razor` owns a `GameBoard` instance and orchestrates the game loop. A new `Tests/Tests.csproj` xUnit project is added to the solution for TDD.

---

## Data Model

```csharp
enum GameStatus { SymbolSelection, Active, PlayerWon, ComputerWon, Draw }

class GameBoard
{
    char[9] Cells        // '\0' = empty, 'X' or 'O' = occupied
    char PlayerSymbol
    char ComputerSymbol
    GameStatus Status    // starts as SymbolSelection
    bool IsPlayerTurn
    void Reset()         // returns to SymbolSelection state
}
```

`GameRules` exposes a single static method:
```csharp
static GameStatus CheckResult(char[] cells, char playerSymbol, char computerSymbol)
```
It scans all 8 win lines. Returns `PlayerWon`, `ComputerWon`, `Draw`, or `Active`.

`ComputerPlayer` exposes a single static method:
```csharp
static int PickCell(char[] cells)
```
Collects all empty indices and returns one chosen at random.

---

## Game Flow

1. App starts → `GameBoard.Status == SymbolSelection`
2. Player picks X or O → sets `PlayerSymbol`, `ComputerSymbol` (opposite), `Status = Active`, `IsPlayerTurn = true`
3. Player clicks an empty cell → `Cells[i] = PlayerSymbol`, `IsPlayerTurn = false`
4. `GameRules.CheckResult` — if terminal, set `Status` and stop
5. `ComputerPlayer.PickCell` → `Cells[i] = ComputerSymbol`
6. `GameRules.CheckResult` — if terminal, set `Status`; otherwise `IsPlayerTurn = true`
7. Player clicks "Restart" → `GameBoard.Reset()`

`IsPlayerTurn = false` during the computer's turn prevents double-clicks. Blazor Server's single-threaded circuit makes additional locking unnecessary.

---

## UI (`Home.razor`)

Uses MudBlazor components throughout. No sub-components — the entire game fits in one Razor file.

### Symbol selection screen (`Status == SymbolSelection`)
- `MudText`: "Choose X or O"
- Two `MudButton` components side by side (X and O)

### Game board screen (all other statuses)
- `MudText` status line:
  - `Active` → "Your turn"
  - `PlayerWon` → "You win"
  - `ComputerWon` → "Computer wins"
  - `Draw` → "Draw"

  Note: "Computer is thinking" is omitted. Because the computer move runs synchronously on the Blazor Server circuit, no render fires between the player's click and the computer's response — showing that state would require an artificial async delay, which is out of scope for this POC.
- 3×3 `MudGrid` of `MudButton` cells showing the cell's symbol or blank
  - Enabled only when `Status == Active && IsPlayerTurn && Cells[i] == '\0'`
- `MudButton` "Restart" → calls `Reset()`

---

## Testing (`Tests/Tests.csproj`)

xUnit project referencing `Library`. `Home.razor` is not unit tested.

### `GameRulesTests`
- All 8 win lines (3 rows, 3 columns, 2 diagonals) for X and for O
- Draw — all 9 cells filled with no winner
- Active — partially filled board, no result yet

### `ComputerPlayerTests`
- Always returns an empty cell index
- Never returns an occupied cell index
- Works when only one cell remains empty
