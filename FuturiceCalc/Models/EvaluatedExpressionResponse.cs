using System.Text.Json.Serialization;

namespace FuturiceCalc.Models
{
    /// <summary>
    /// response for evaluated expression
    /// </summary>
    public class EvaluatedExpressionResponse
    {
        [JsonPropertyName("error")]
        public bool Error { get; set; }

        [JsonPropertyName("result")]
        public decimal Result { get; set; }
    }
}
