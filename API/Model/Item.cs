namespace API.Model
{
  public class Item
  {
    public int Id { get; set; }
    public int IdPedido { get; set; }
    public string Descricao { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Quantidade { get; set; }
  }
}
