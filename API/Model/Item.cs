using System.Text.Json.Serialization;

namespace API.Model
{
  public class Item
  {
    /// <summary>
    /// Descrição
    /// </summary>
    [JsonPropertyName("descricao")]
    public string Descricao { get; set; }

    /// <summary>
    /// Preço Unitário
    /// </summary>
    [JsonPropertyName("precoUnitario")]
    public decimal PrecoUnitario { get; set; }

    /// <summary>
    /// Quantidade
    /// </summary>
    [JsonPropertyName("qtd")]
    public decimal Quantidade { get; set; }
  }
}
