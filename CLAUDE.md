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
