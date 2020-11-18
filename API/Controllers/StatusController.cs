using API.Service;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  /// <summary>
  /// Status Controller
  /// </summary>
  [ApiController]
  [Route("api/[controller]")]
  public class StatusController : ControllerBase
  {
    private readonly IStatusService _statusService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="statusService">status service</param>
    public StatusController(IStatusService statusService)
    {
      _statusService = statusService;
    }

    /// <summary>
    /// Update Status
    /// </summary>
    /// <param name="status">Status request</param>
    /// <returns>Status response</returns>
    [HttpPost]
    public async Task<StatusResponse> Post(StatusRequest status) => await _statusService.Update(status).ConfigureAwait(false);
  }
}
