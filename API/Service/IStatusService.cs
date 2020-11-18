using API.ViewModel;
using System.Threading.Tasks;

namespace API.Service
{
  /// <summary>
  /// Status Service
  /// </summary>
  public interface IStatusService
  {
    /// <summary>
    /// Update Pedido Status
    /// </summary>
    /// <param name="status">status of pedido</param>
    /// <returns>status response</returns>
    Task<StatusResponse> Update(StatusRequest status);
  }
}
