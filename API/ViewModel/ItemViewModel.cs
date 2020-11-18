using API.Model;
using System.Text.Json.Serialization;

namespace API.ViewModel
{
  /// <summary>
  /// View Model Item
  /// </summary>
  public class ItemViewModel
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public ItemViewModel() { }

    /// <summary>
    /// Constructor (convert from db)
    /// </summary>
    /// <param name="item">Db Item</param>
    public ItemViewModel(Item item)
    {
      if (item is null) return;

      Descricao = item.Descricao;
      PrecoUnitario = item.PrecoUnitario;
      Quantidade = item.Quantidade;
    }

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
