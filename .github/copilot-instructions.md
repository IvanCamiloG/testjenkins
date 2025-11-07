## Quick orientation

This repository contains a very small .NET console app and a test project. Key files:

- `ConsoleApp1/` — main console app (target: net9.0). Example implementation: `ConsoleApp1/calculadora.cs`.
- `TestTUnit/` — test project that references the app via a ProjectReference (`TestTUnit/TestTUnit.csproj`) and uses the TUnit test framework (package `TUnit` in the csproj).
- `ConsoleApp1.sln` — solution file at the repo root.

Keep these paths in mind when writing or modifying code, tests, or CI steps.

## Big-picture architecture and data flow (short)

- Single executable project (`ConsoleApp1`) contains implementation classes (e.g. `calculadora`) used by the test project.
- `TestTUnit` references the main project directly with `<ProjectReference Include="..\ConsoleApp1\ConsoleApp1.csproj" />` so tests run against compiled code from the sibling project.
- Tests use the external `TUnit` package and rely on attribute-based test / lifecycle hooks (see `TestTUnit/GlobalSetup.cs`).

Why this layout: the test project is intentionally a separate assembly so tests exercise the public surface of `ConsoleApp1` via a project reference rather than re-implementing logic in test code.

## Developer workflows (commands you can run locally)

- Restore + build solution (run from repo root):

```powershell
dotnet restore "d:\testc\ConsoleApp1.sln"
dotnet build "d:\testc\ConsoleApp1.sln" -c Debug
```

- Run the console app (from repo root):

```powershell
dotnet run --project "d:\testc\ConsoleApp1\ConsoleApp1.csproj"
```

- Run tests (from repo root). Prefer running the test project directly:

```powershell
dotnet test "d:\testc\TestTUnit\TestTUnit.csproj" -c Debug
```

Notes: the test project targets net9.0 (see `TargetFramework`) and references `TUnit` via NuGet. Ensure a recent .NET SDK (compatible with net9.0) is installed on the machine running these commands.

## Project-specific conventions and patterns

- Test framework: `TUnit` (not xUnit / NUnit). Tests and lifecycle hooks use attributes such as `[Test]`, `[Arguments(...)]`, `[Before(TestSession)]`, `[After(TestSession)]`, and `[Retry(n)]`. See `TestTUnit/GlobalSetup.cs` and `TestTUnit/Tests.cs` for examples.
- Implicit usings and nullable are enabled in csproj files (`<ImplicitUsings>enable</ImplicitUsings>` and `<Nullable>enable</Nullable>`).
- The example code uses a lowercase class name `calculadora` in `ConsoleApp1/calculadora.cs` — be careful to follow existing naming in code when editing to minimize churn, but prefer PascalCase for new types unless intentionally matching existing names.
- Tests reference the production code by creating instances directly (e.g. `new calculadora()`), not via DI container. If you add DI, update tests accordingly.

## Integration points and external dependencies

- NuGet: `TUnit` package in `TestTUnit/TestTUnit.csproj`.
- No other external services or databases are referenced in the repository.

## Files to inspect when making changes

- `ConsoleApp1/calculadora.cs` — simple example implementation.
- `ConsoleApp1/ConsoleApp1.csproj` — project settings (TargetFramework, implicit usings).
- `TestTUnit/TestTUnit.csproj` — test package references and ProjectReference to the app.
- `TestTUnit/Tests.cs` — example tests and attribute usage.
- `TestTUnit/GlobalSetup.cs` — global test hooks and assembly-level attributes.

## Common pitfalls observed (so the agent can be proactive)

- Tests use attribute-based params (`[Arguments]`) — ensure you preserve argument order and attribute names when editing tests.
- The repo contains small, hand-written examples that may not conform to strict naming or formatting rules (for example, `calculadora` uses lowercase class name and `TestTUnit/Tests.cs` currently shows non-compiling syntax in a couple of places). Before making large changes, run `dotnet build` and `dotnet test` to surface compile errors.

## How AI agents should behave here (short checklist)

1. Read `ConsoleApp1/` and `TestTUnit/` before editing to understand how tests reference production code.
2. Run `dotnet build` then `dotnet test` after changes; fix compilation errors caused by edits rather than silently changing test signatures.
3. Preserve existing attribute-based test patterns (e.g., `[Arguments]`, `[Before]`, `[After]`, `[Retry]`).
4. When adding new files, follow repo layout: place implementation in `ConsoleApp1/` and tests in `TestTUnit/`.

If anything in this document is unclear or you'd like more examples (e.g., CI snippets, expected test outputs, or a corrected sample test), tell me which area to expand and I will iterate.
