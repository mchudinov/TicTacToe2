# Implementation Steps

| Step | Title | Status |
|------|-------|--------|
| 1 | Set up Tests project and GameBoard | Backlog |
| 2 | GameRules with TDD | Backlog |
| 3 | ComputerPlayer with TDD | Backlog |
| 4 | Game UI and full game loop | Backlog |

---

## Step 1 — Set up Tests project and GameBoard

Add a new `Tests/Tests.csproj` xUnit project to the solution (`TicTacToe.slnx`) that references `Library`. Implement the `GameBoard` class in `Library/` — a pure state container with no logic: `char[9] Cells`, `char PlayerSymbol`, `char ComputerSymbol`, `GameStatus Status` (enum: `SymbolSelection`, `Active`, `PlayerWon`, `ComputerWon`, `Draw`), `bool IsPlayerTurn`, and a `Reset()` method that returns the board to its initial `SymbolSelection` state.

---

## Step 2 — GameRules with TDD

Implement `GameRules` as a static class in `Library/` with a single method `CheckResult(char[] cells, char playerSymbol, char computerSymbol)` returning `GameStatus`. Write `GameRulesTests` in `Tests/` first (TDD): cover all 8 win lines (3 rows, 3 columns, 2 diagonals) for both X and O, a full-board draw, and an active in-progress board. Then implement `CheckResult` to make all tests pass.

---

## Step 3 — ComputerPlayer with TDD

Implement `ComputerPlayer` as a static class in `Library/` with a single method `PickCell(char[] cells)` that collects empty indices and returns one at random. Write `ComputerPlayerTests` in `Tests/` first (TDD): verify the method always returns an empty index, never an occupied one, and works correctly when only one cell remains. Then implement `PickCell` to make all tests pass.

---

## Step 4 — Game UI and full game loop

Update `Home.razor` to render two screens using MudBlazor components:

- **Symbol selection** (`Status == SymbolSelection`): `MudText` heading + two `MudButton` components (X and O) that set `PlayerSymbol`, `ComputerSymbol`, and start the game.
- **Game board** (all other statuses): `MudText` status line ("Your turn" / "You win" / "Computer wins" / "Draw"), a 3×3 `MudGrid` of `MudButton` cells (enabled only when `Active` and it is the player's turn and the cell is empty), and a `MudButton` "Restart" that calls `Reset()`.

Wire the full game loop: player clicks a cell → write symbol → `GameRules.CheckResult` → if still active, `ComputerPlayer.PickCell` → write symbol → `GameRules.CheckResult` → update status.
