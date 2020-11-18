using API.Service;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StatusController : ControllerBase
  {
    private readonly ILogger<StatusController> _logger;
    private readonly IStatusService _statusService;

    public StatusController(ILogger<StatusController> logger, IStatusService statusService)
    {
      _logger = logger;

      _statusService = statusService;
    }

    [HttpPost]
    public async Task<StatusResponse> Post(StatusRequest status) => await _statusService.Update(status).ConfigureAwait(false);
  }
}
