using API.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Service
{
  public interface IPedidoService
  {
    /// <summary>
    /// Get All Items
    /// </summary>
    /// <returns>Pedidos</returns>
    Task<IEnumerable<PedidoViewModel>> GetAll();

    /// <summary>
    /// Get Items By Pedido Id
    /// </summary>
    /// <param name="id">Pedido Id</param>
    /// <returns>Pedido</returns>
    Task<PedidoViewModel> Get(int id);

    /// <summary>
    /// Insert|Update Pedido
    /// </summary>
    /// <param name="pedido">Pedido</param>
    /// <returns>true on success</returns>
    Task<bool> InsertUpdate(PedidoViewModel pedido);

    /// <summary>
    /// Delete Pedido
    /// </summary>
    /// <param name="id">Pedido Id</param>
    /// <returns>true on success</returns>
    Task<bool> Delete(int id);
  }
}
