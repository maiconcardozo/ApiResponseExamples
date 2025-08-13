using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Swagger
{
    public class SucessDetails : ProblemDetails
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? Detail { get; set; }
        public string? Instance { get; set; }
        public object? Data { get; set; }
    }
}
