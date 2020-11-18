using API.Service;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
  /// <summary>
  /// Pedido Controller
  /// </summary>
  [ApiController]
  [Route("api/[controller]")]
  public class PedidoController : ControllerBase
  {
    private readonly IPedidoService _pedidoService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pedidoService">pedido service</param>
    public PedidoController(IPedidoService pedidoService)
    {
      _pedidoService = pedidoService;
    }

    /// <summary>
    /// Get All
    /// </summary>
    /// <returns>Pedidos</returns>
    [HttpGet("[action]")]
    public async Task<IEnumerable<PedidoViewModel>> GetAll() => await _pedidoService.GetAll().ConfigureAwait(false);

    /// <summary>
    /// Get Single
    /// </summary>
    /// <param name="id">Pedido Id</param>
    /// <returns>Pedido</returns>
    [HttpGet("{id}")]
    public async Task<PedidoViewModel> Get(int id) => await _pedidoService.Get(id).ConfigureAwait(false);

    /// <summary>
    /// Add Pedido
    /// </summary>
    /// <param name="pedido">pedido</param>
    /// <returns>true on success</returns>
    [HttpPost]
    public async Task<bool> Post(PedidoViewModel pedido) => await _pedidoService.InsertUpdate(pedido).ConfigureAwait(false);

    /// <summary>
    /// Update Pedido
    /// </summary>
    /// <param name="pedido">pedido</param>
    /// <returns>true on success</returns>
    [HttpPut]
    public async Task<bool> Put(PedidoViewModel pedido) => await _pedidoService.InsertUpdate(pedido).ConfigureAwait(false);

    /// <summary>
    /// Delete Pedido
    /// </summary>
    /// <param name="id">Pedido Id</param>
    /// <returns>true on success</returns>
    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id) => await _pedidoService.Delete(id).ConfigureAwait(false);
  }
}
