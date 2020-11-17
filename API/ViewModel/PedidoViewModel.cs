using API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace API.ViewModel
{
  public class PedidoViewModel
  {
    public PedidoViewModel() { }

    public PedidoViewModel(Item item)
    {
      if (item is null) return;

      Numero = item.IdPedido.ToString();
      Itens = new List<ItemViewModel>() { new ItemViewModel(item) };
    }

    public PedidoViewModel(IGrouping<int, Item> group)
    {
      if (group is null) return;

      Numero = group.Key.ToString();
      Itens = group.Select(x => new ItemViewModel(x));
    }

    /// <summary>
    /// Número do Pedido
    /// </summary>
    [JsonPropertyName("pedido")]
    public string Numero { get; set; }

    /// <summary>
    /// Itens do Pedido
    /// </summary>
    [JsonPropertyName("itens")]
    public IEnumerable<ItemViewModel> Itens { get; set; }
  }
}
