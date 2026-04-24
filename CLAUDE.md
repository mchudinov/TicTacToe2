# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Run the web app (available at http://localhost:8089)
dotnet run --project Web/Web.csproj

# Build the solution
dotnet build TicTacToe.slnx

# Build for release / Docker
dotnet publish Web/Web.csproj -c Release

# Docker build
docker build -t tictactoe -f Web/Dockerfile .
```

No test projects exist yet.

## Architecture

Two projects in `TicTacToe.slnx`:

- **`Library/`** — shared utility class library. Contains configuration extension methods (`Extensions.cs`), Azure Blob Storage name sanitization, and OpenTelemetry setup (activated when Azure Monitor connection string is present).
- **`Web/`** — ASP.NET Core 10 Blazor Server application. Entry point is `Program.cs`, which wires up Serilog, MudBlazor, Azure OpenAI client, and the `Settings` model from `appsettings.json`. Serves on port 8089.

### Frontend

Blazor Server with InteractiveServer rendering. MudBlazor 9.3.0 provides all UI components. Dark mode state is persisted in localStorage. The router (`Routes.razor`) handles navigation; `App.razor` is the root HTML document.

### Game implementation status

The game logic is **not yet implemented**. `Web/Components/Pages/Home.razor` is a placeholder. The POC specification in `docs/poc.md` defines the intended single-player (vs. computer) game: symbol selection, a 3×3 clickable grid, and two computer AI levels (random moves vs. strategic block/win). Azure OpenAI (Azure.AI.OpenAI 2.1.0) is already wired into DI for potential use in an AI-powered computer player.

### Observability

- Structured logging via Serilog (console + debug sinks).
- OpenTelemetry tracing via `Azure.Monitor.OpenTelemetry.AspNetCore` — only activates when `APPLICATIONINSIGHTS_CONNECTION_STRING` is set.
- Diagnostic endpoints: `/livez`, `/uptime`, `/error` (registered in `Library/Extensions.cs`).

### Configuration

`Settings.cs` maps `appsettings.json`. Azure OpenAI credentials (endpoint, API key, deployment name) live under the `AzureOpenAI` section.


## Development Rules

### Test-Driven Development

Always follow TDD: write a failing test first, verify it fails for the right reason, then write the minimal production code to make it pass. Never write production code before a failing test exists. Delete any production code written before its test and start over.

## Project Instructions

### Context7 Integration

Always use Context7 MCP when I need library/API documentation, code generation, setup, or configuration steps without me having to explicitly ask.

### Plan Step

When the user asks to "plan a step" (or equivalent phrasing like "add a step", "create a step"):
1. Add the step to `docs/plans/steps.md` — a new row in the table and a full detail section at the bottom.
2. Create a GitHub project item in the TicTacToe project with title "Step-N Short description", a short description body, and status set to Backlog.

Both actions are always done together, without the user having to ask for each separately.

### GitHub integration

When creating items in GitHub always use  TicTacToe GitHub project. Create items in Backlog and give a name to each item "Step-number short description". Add a short description to each item. Try to figure out what was the latest used step number in the current session and add +1 to each step.

When the user asks to work on a Step:
1. Create a dedicated GitHub branch named after the step (e.g. `Step-1-Create-AccountSnapshot-data-model`)
2. Move the corresponding GitHub project item to "In-progress" state
3. Do all coding on that branch

When programming is done:
1. Move the GitHub project item to "In-review" state
2. Create a Pull Request for the branch — the user will approve and merge it; never merge it yourself

When the user asks to check if a PR is approved:
1. Check PR review status with `gh pr view <number> --json reviewDecision,mergeStateStatus`
2. If approved and merged: switch to `main`, pull latest, delete the local and remote feature branch, move the GitHub project item to "Done" state, then remind the user to run `/compact`
3. If not yet approved: report the current status and wait for the user to ask again