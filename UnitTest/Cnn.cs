using API.Connection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UnitTest
{
  public static class Cnn
  {
    public static IDbCnn GetCnn(ILoggerFactory loggerFactory)
    {
      var config = new ConfigurationBuilder().AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true).Build();
      return new PedidoSqlCnn(loggerFactory?.CreateLogger<PedidoSqlCnn>(), config);
    }
  }
}
