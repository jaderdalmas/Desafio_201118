using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.ViewModel
{
  public class StatusRequest
  {
    /// <summary>
    /// Status
    /// </summary>
    [JsonPropertyName("status")]
    [Required]
    public string Status { get; set; }

    /// <summary>
    /// Itens Aprovados
    /// </summary>
    [JsonPropertyName("itensAprovados")]
    [Range(1, int.MaxValue)]
    public int ItensAprovados { get; set; }

    /// <summary>
    /// Valor Aprovado
    /// </summary>
    [JsonPropertyName("valorAprovado")]
    [Range(0, (double)decimal.MaxValue)]
    public decimal ValorAprovado { get; set; }

    /// <summary>
    /// Número do Pedido
    /// </summary>
    [JsonPropertyName("pedido")]
    [Required]
    public string Pedido { get; set; }
  }
}
