using API.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace API.Model
{
  public class Item
  {
    public Item() { }

    public Item(PedidoViewModel pedido, ItemViewModel item)
    {
      IdPedido = int.Parse(pedido.Numero);
      Descricao = item.Descricao;
      PrecoUnitario = item.PrecoUnitario;
      Quantidade = item.Quantidade;
    }

    [Key]
    [Range(0, int.MaxValue)]
    public int Id { get; set; }

    [Range(0, int.MaxValue)]
    public int IdPedido { get; set; }

    public string Descricao { get; set; }

    [Range(0, (double)decimal.MaxValue)]
    public decimal PrecoUnitario { get; set; }

    [Range(0, (double)decimal.MaxValue)]
    public decimal Quantidade { get; set; }
  }
}
