using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace API.Service
{
  /// <summary>
  /// Inject Services
  /// </summary>
  [ExcludeFromCodeCoverage]
  public static class IoC
  {
    /// <summary>
    /// Add Services in the service collection
    /// </summary>
    /// <param name="services">service collection</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
      services.AddScoped<IPedidoService, PedidoService>();

      return services;
    }
  }
}
