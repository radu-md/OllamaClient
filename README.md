# OllamaClient

## Prerequisites

- Docker Desktop installed and running
- .NET SDK 10.0 (see `global.json` / `.csproj`)

## Run Ollama in Docker Desktop

1. Start the Ollama container:

    ```bash
    docker run -d --name ollama -p 11434:11434 ollama/ollama:latest
    ```

2. Pull the default model used by this client (`llama3.2`):

    ```bash
    docker exec -it ollama ollama pull llama3.2
    ```

After the model is downloaded, the Ollama API will be available at:

- http://localhost:11434

## Run the OllamaClient console app

From the repository root:

```bash
dotnet restore
dotnet run --project src/OllamaClientApp --configuration Debug
```