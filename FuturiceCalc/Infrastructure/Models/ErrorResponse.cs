using System.Text.Json.Serialization;

namespace FuturiceCalc.Infrastructure.Models
{
    public class ErrorResponse
    {
        [JsonPropertyName("error")]
        public bool Error { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
