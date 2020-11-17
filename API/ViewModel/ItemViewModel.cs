using API.Model;
using System.Text.Json.Serialization;

namespace API.ViewModel
{
  public class ItemViewModel
  {
    public ItemViewModel() { }

    public ItemViewModel(Item item)
    {
      if (item is null) return;

      Descricao = item.Descricao;
      PrecoUnitario = item.PrecoUnitario;
      Quantidade = item.Quantidade;
    }

    /// <summary>
    /// Descri��o
    /// </summary>
    [JsonPropertyName("descricao")]
    public string Descricao { get; set; }

    /// <summary>
    /// Pre�o Unit�rio
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
