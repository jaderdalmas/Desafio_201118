using API.Extension;
using API.Model;
using API.Repository;
using API.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  public class PedidoService : IPedidoService
  {
    private readonly IItemRepository _itemRepository;

    public PedidoService(IItemRepository itemRepository)
    {
      _itemRepository = itemRepository;
    }

    public async Task<IEnumerable<PedidoViewModel>> GetAll()
    {
      var list = await _itemRepository.GetAll().ConfigureAwait(false);

      return list?.GroupBy(x => x.IdPedido).Select(x => new PedidoViewModel(x)) ?? new List<PedidoViewModel>();
    }

    public async Task<PedidoViewModel> Get(int id)
    {
      if (id < 1)
        return null;

      var list = await _itemRepository.GetByPedido(id).ConfigureAwait(false);

      return list?.GroupBy(x => x.IdPedido).Select(x => new PedidoViewModel(x)).FirstOrDefault();
    }

    public async Task<bool> InsertUpdate(PedidoViewModel pedido)
    {
      if (pedido is null || !pedido.IsValid() || pedido.Itens?.Any() != true || !int.TryParse(pedido.Numero, out int idPedido))
        return false;

      Delete(idPedido).Wait();
      var list = pedido.Itens.Select(x => new Item(pedido, x));
      var result = new List<int>();
      foreach (var item in list.AsParallel())
        result.Add(await _itemRepository.Insert(item).ConfigureAwait(false));

      return result.All(x => x > 0);
    }

    public async Task<bool> Delete(int id)
    {
      if (id < 1)
        return false;

      return await _itemRepository.DeleteByPedido(id).ConfigureAwait(false);
    }
  }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
