using API.Service;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PedidoController : ControllerBase
  {
    private readonly ILogger<PedidoController> _logger;
    private readonly IPedidoService _pedidoService;

    public PedidoController(ILogger<PedidoController> logger, IPedidoService pedidoService)
    {
      _logger = logger;

      _pedidoService = pedidoService;
    }

    [HttpGet("[action]")]
    public async Task<IEnumerable<PedidoViewModel>> GetAll() => await _pedidoService.GetAll().ConfigureAwait(false);

    [HttpGet("{id}")]
    public async Task<PedidoViewModel> Get(int id) => await _pedidoService.Get(id).ConfigureAwait(false);

    [HttpPost]
    public async Task<bool> Post(PedidoViewModel pedido) => await _pedidoService.InsertUpdate(pedido).ConfigureAwait(false);

    [HttpPut]
    public async Task<bool> Put(PedidoViewModel pedido) => await _pedidoService.InsertUpdate(pedido).ConfigureAwait(false);

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id) => await _pedidoService.Delete(id).ConfigureAwait(false);
  }
}
