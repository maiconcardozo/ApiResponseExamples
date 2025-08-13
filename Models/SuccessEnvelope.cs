using System.Text.Json.Serialization;

namespace ApiResponseExamplesDemo.Models;

public class SuccessEnvelope<T>
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("detail")]
    public string Detail { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("instance")]
    public string Instance { get; set; } = string.Empty;

    public static SuccessEnvelope<T> Ok(T data, string instance, string detail = "Operação concluída com sucesso") =>
        new()
        {
            Status = StatusCodes.Status200OK,
            Title = "OK",
            Detail = detail,
            Data = data,
            Instance = instance
        };
}