using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.ViewModel
{
  public class Pedido
  {
    /// <summary>
    /// N�mero do Pedido
    /// </summary>
    [JsonPropertyName("pedido")]
    public int Numero { get; set; }

    /// <summary>
    /// Itens do Pedido
    /// </summary>
    [JsonPropertyName("itens")]
    public List<Item> Itens { get; set; }
  }
}
