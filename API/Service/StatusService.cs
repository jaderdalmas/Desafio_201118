using API.Extension;
using API.Repository;
using API.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  public class StatusService : IStatusService
  {
    private readonly IItemRepository _itemRepository;

    public StatusService(IItemRepository itemRepository)
    {
      _itemRepository = itemRepository;
    }

    public async Task<StatusResponse> Update(StatusRequest status)
    {
      if (status is null || !status.IsValid())
        return null;

      var result = new StatusResponse(status.Pedido);
      if (!int.TryParse(status.Pedido, out int idPedido))
      {
        result.Status.Add("CODIGO_PEDIDO_INVALIDO");
        return result;
      }

      var pedido = await _itemRepository.GetByPedido(idPedido).ConfigureAwait(false);
      if (pedido?.Any() != true)
      {
        result.Status.Add("CODIGO_PEDIDO_INVALIDO");
        return result;
      }

      if (status.Status.ToUpperInvariant() == "REPROVADO")
        result.Status.Add(status.Status.ToUpperInvariant());

      if (status.Status.ToUpperInvariant() == "APROVADO")
      {
        if (status.ItensAprovados != pedido.Count())
        {
          if (status.ItensAprovados < pedido.Count())
            result.Status.Add("APROVADO_QTD_A_MENOR");
          if (status.ItensAprovados > pedido.Count())
            result.Status.Add("APROVADO_QTD_A_MAIOR");
        }
        var valorPedido = pedido.Sum(x => x.PrecoUnitario * x.Quantidade);
        if (status.ValorAprovado != valorPedido)
        {
          if (status.ValorAprovado < valorPedido)
            result.Status.Add("APROVADO_VALOR_A_MENOR");
          if (status.ValorAprovado > valorPedido)
            result.Status.Add("APROVADO_VALOR_A_MAIOR");
        }

        if (!result.Status.Any())
          result.Status.Add(status.Status.ToUpperInvariant());
      }

      return result;
    }
  }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
