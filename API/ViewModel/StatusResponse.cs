using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.ViewModel
{
  public class StatusResponse
  {
    public StatusResponse()
    {
      Status = new List<string>();
    }

    public StatusResponse(string pedido)
    {
      Pedido = pedido;
      Status = new List<string>();
    }

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
