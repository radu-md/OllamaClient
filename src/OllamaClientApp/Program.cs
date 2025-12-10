using System.Net.Http.Json;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:11434")
};

const string modelName = "llama3.2";

Console.WriteLine("Trimit un prompt de test către Ollama...");

var requestBody = new
{
    model = modelName,
    prompt = "Explică foarte pe scurt, în română, ce este Local AI.",
    stream = false
};

using var response = await httpClient.PostAsJsonAsync("/api/generate", requestBody);
response.EnsureSuccessStatusCode();

var payload = await response.Content.ReadFromJsonAsync<GenerateResponse>()
              ?? throw new InvalidOperationException("Răspuns gol de la Ollama.");

Console.WriteLine("Răspuns de la model:");
Console.WriteLine(payload.Response);

public sealed class GenerateResponse
{
    public required string Model { get; init; }
    public required string Response { get; init; }
    public bool Done { get; init; }
}
