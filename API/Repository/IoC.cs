using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace API.Repository
{
  /// <summary>
  /// Inject Repositories
  /// </summary>
  [ExcludeFromCodeCoverage]
  public static class IoC
  {
    /// <summary>
    /// Add Repositories in the service collection
    /// </summary>
    /// <param name="services">service collection</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
      services.AddScoped<IItemRepository, ItemRepository>();
      services.AddScoped<IPedidoRepository, PedidoRepository>();

      return services;
    }
  }
}
