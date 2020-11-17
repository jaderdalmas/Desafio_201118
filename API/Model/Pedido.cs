using System.ComponentModel.DataAnnotations;

namespace API.Model
{
  public class Pedido
  {
    public int Id { get; set; }

    [Required]
    public string Status { get; set; }
  }
}
