using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.ViewModel
{
  public class StatusResponse
  {
    /// <summary>
    /// Status
    /// </summary>
    [JsonPropertyName("status")]
    public IList<string> Status { get; set; }

    /// <summary>
    /// Número do Pedido
    /// </summary>
    [JsonPropertyName("pedido")]
    public string Pedido { get; set; }
  }
}
