using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.ViewModel
{
  /// <summary>
  /// View Model Status Response
  /// </summary>
  public class StatusResponse
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public StatusResponse()
    {
      Status = new List<string>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pedido">pedido id</param>
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
