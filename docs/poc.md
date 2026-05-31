# Tic-Tac-Toe Web POC

## Overview
This Proof of Concept (POC) demonstrates a simple web-based Tic-Tac-Toe game where a single user plays against the computer in a browser.

---

## Game Setup

- The game starts with a **symbol selection screen**.
- The user chooses to play as:
  - **X**, or
  - **O**
- After selection, the game board is displayed.
- The **user always plays first**.

---

## Game Board

- A **3×3 grid** is shown.
- Each cell is clickable using the mouse.
- Empty cells are interactive; occupied cells are disabled.

---

## Gameplay Flow

### 1. User Move
- The user clicks on an empty cell.
- The selected symbol (**X or O**) is placed in that cell.
- The system validates the move.

### 2. Game State Check
After the user move:
- Check if the user has won.
- Check if the board is full (draw).
- If the game is not finished → proceed to computer move.

---

### 3. Computer Move
- The computer automatically selects a move.
- POC logic options:
  - **Basic**: Random empty cell
  - **Improved (optional)**:
    - Take winning move if available
    - Block user's winning move

- The computer places its symbol on the board.

---

### 4. Game State Check (Again)
After the computer move:
- Check if the computer has won.
- Check if the board is full (draw).

---

## Win Conditions

A player wins if they align three of their symbols:
- Horizontally
- Vertically
- Diagonally

If all 9 cells are filled with no winner → **Draw**

---

## UI Elements

### Status Display
Dynamic text indicating current state:
- “Choose X or O”
- “Your turn”
- “Computer is thinking”
- “You win”
- “Computer wins”
- “Draw”

### Controls
- **Restart Game Button**
  - Clears the board
  - Returns to symbol selection

---

## Interaction Model

- Input method: **Mouse clicks**
- Immediate visual feedback after each move
- Disabled interaction during computer turn

---

## Technical Scope (POC Level)

### Frontend
- HTML for structure
- CSS for layout (grid)
- JavaScript for:
  - Game state management
  - Event handling (clicks)
  - Win condition checks
  - Computer move logic

### State Model (Simplified)
- Board: Array of 9 cells
- Player symbol: X or O
- Computer symbol: opposite of player
- Current turn: user / computer
- Game status: active / win / draw

---

## POC Goal

Demonstrate:
- Basic game loop (user → computer → check state)
- Simple AI behavior
- Interactive browser-based gameplay
- Clear UI feedback and control flow