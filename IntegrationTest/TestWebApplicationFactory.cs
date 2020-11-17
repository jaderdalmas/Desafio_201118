using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace IntegrationTest
{
  [ExcludeFromCodeCoverage]
  public class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      if (builder is null) return;
      builder.ConfigureAppConfiguration((builderContext, config) =>
      {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
      });
    }
  }
}
