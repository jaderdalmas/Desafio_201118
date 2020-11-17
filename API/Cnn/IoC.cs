using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace API.Connection
{
  /// <summary>
  /// Inject Connections
  /// </summary>
  [ExcludeFromCodeCoverage]
  public static class IoC
  {
    /// <summary>
    /// Add Connections in the service collection
    /// </summary>
    /// <param name="services">service collection</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddConns(this IServiceCollection services)
    {
      services.AddScoped<IDbCnn, PedidoSqlCnn>();

      return services;
    }
  }
}
