using API.Extension;
using API.Model;
using API.Repository;
using API.ViewModel;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service
{
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

    public async Task<IEnumerable<PedidoViewModel>> Get(int id)
    {
      if (id < 1)
        return null;

      var list = await _itemRepository.GetByPedido(id).ConfigureAwait(false);

      return list?.GroupBy(x => x.IdPedido).Select(x => new PedidoViewModel(x)) ?? new List<PedidoViewModel>();
    }

    public async Task<bool> InsertUpdate(PedidoViewModel pedido)
    {
      if (pedido is null || !pedido.IsValid() || !pedido.Itens.Any() || int.TryParse(pedido.Numero, out int idPedido))
        return false;

      await Delete(idPedido).ConfigureAwait(false);
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
}
