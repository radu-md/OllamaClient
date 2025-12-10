# OllamaClient Copilot Notes

## Architecture

- Single console target in `src/OllamaClientApp`; the solution only wires this project.
- `Program.cs` uses top-level async statements; prefer local static methods or new files for complex flows instead of introducing `Main`.
- HTTP integration hits Ollama's `/api/generate` endpoint; request body currently includes `model`, `prompt`, and `stream`.
- `GenerateResponse` class at the end of `Program.cs` maps `model`, `response`, and `done`; expand it carefully if the Ollama schema changes.
- All logs are in Romanian because the sample prompt is Romanian; preserve or parameterize localization intentionally.

## External Requirements

- The app assumes an Ollama instance on `http://localhost:11434`; follow README Docker instructions before running.
- Default model name is `llama3.2`; confirm availability or expose configuration rather than hardcoding a new name.
- Network failures bubble via `EnsureSuccessStatusCode`; emit friendlier diagnostics when adding retries or richer error handling.

## Build & Run

- Use `dotnet restore` once, then `dotnet build OllamaClient.sln` for verification.
- Preferred entrypoint command: `dotnet run --project src/OllamaClientApp --configuration Debug`.
- Target framework is `net10.0`; update the csproj and global.json together if you downlevel.
- No automated tests yet; rely on manual invocation against a local Ollama server.

## Coding Patterns

- `HttpClient` is instantiated per run and disposed with `using`; keep that pattern or introduce a singleton through DI intentionally.
- Payloads are sent with `PostAsJsonAsync`; reuse `System.Net.Http.Json` extensions for new endpoints for consistent serialization.
- When adding DTOs, use `required` init-only properties to align with nullable enablement.
- Stick to ASCII unless extending existing Romanian text that already contains diacritics.
- Keep comments short; add them only for non-obvious API interactions.

## Extending Functionality

- For new Ollama endpoints, create dedicated request/response types in the same folder and call them from `Program.cs`.
- Surface configuration (model name, prompt text, base address) via `appsettings` and `Microsoft.Extensions.Configuration` if you start using those packages.
- Log user-facing messages with `Console.WriteLine` and keep them localized consistently.
- When adding streaming support, reuse the existing HTTP client and guard the `stream` flag changes with feature toggles or constants.
- Document any new workflow additions back in the README to keep environment setup in sync.
