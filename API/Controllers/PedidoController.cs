using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PedidoController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    private readonly ILogger<PedidoController> _logger;
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoController(ILogger<PedidoController> logger, IPedidoRepository pedidoRepository)
    {
      _logger = logger;

      _pedidoRepository = pedidoRepository;
    }

    [HttpGet]
    public IEnumerable<Pedido> Get()
    {
      return _pedidoRepository.GetAll();
    }
  }
}
