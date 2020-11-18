using API.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace API.Model
{
  /// <summary>
  /// Item
  /// </summary>
  public class Item
  {
    /// <summary>
    /// Contructor
    /// </summary>
    public Item() { }

    /// <summary>
    /// Constructor (convert from view)
    /// </summary>
    /// <param name="pedido">view pedido</param>
    /// <param name="item">view item</param>
    public Item(PedidoViewModel pedido, ItemViewModel item)
    {
      IdPedido = int.Parse(pedido.Numero);
      Descricao = item.Descricao;
      PrecoUnitario = item.PrecoUnitario;
      Quantidade = item.Quantidade;
    }

    /// <summary>
    /// Item Id
    /// </summary>
    [Key]
    [Range(0, int.MaxValue)]
    public int Id { get; set; }

    /// <summary>
    /// Pedido Id
    /// </summary>
    [Range(0, int.MaxValue)]
    public int IdPedido { get; set; }

    /// <summary>
    /// Descrição
    /// </summary>
    public string Descricao { get; set; }

    /// <summary>
    /// Preço Unitário
    /// </summary>
    [Range(0, (double)decimal.MaxValue)]
    public decimal PrecoUnitario { get; set; }

    /// <summary>
    /// Quantidade
    /// </summary>
    [Range(0, (double)decimal.MaxValue)]
    public decimal Quantidade { get; set; }
  }
}
