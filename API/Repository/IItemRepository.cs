using API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
  /// <summary>
  /// Item Repository
  /// </summary>
  public interface IItemRepository
  {
    /// <summary>
    /// Get All Items
    /// </summary>
    /// <returns>Items</returns>
    Task<IEnumerable<Item>> GetAll();

    /// <summary>
    /// Get Items By Pedido Id
    /// </summary>
    /// <param name="idPedido">Pedido Id</param>
    /// <returns>Items</returns>
    Task<IEnumerable<Item>> GetByPedido(int idPedido);

    /// <summary>
    /// Get Item By Id
    /// </summary>
    /// <param name="id">Item Id</param>
    /// <returns>Item</returns>
    Task<Item> Get(int id);

    /// <summary>
    /// Insert Item
    /// </summary>
    /// <param name="item">Item</param>
    /// <returns>Pedido Id</returns>
    Task<int> Insert(Item item);

    /// <summary>
    /// Update Item
    /// </summary>
    /// <param name="item">Item</param>
    /// <returns>true on success</returns>
    Task<bool> Update(Item item);

    /// <summary>
    /// Delete Item
    /// </summary>
    /// <param name="id">Item Id</param>
    /// <returns>true on success</returns>
    Task<bool> Delete(int id);

    /// <summary>
    /// Delete Items
    /// </summary>
    /// <param name="idPedido">Pedido Id</param>
    /// <returns>true on success</returns>
    Task<bool> DeleteByPedido(int idPedido);
  }
}
