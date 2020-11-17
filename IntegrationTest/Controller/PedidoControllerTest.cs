using API;
using API.ViewModel;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Controller
{
  public class PedidoControllerTest : IClassFixture<TestApplicationFactory<Startup>>
  {
    private readonly TestApplicationFactory<Startup> _factory;

    public PedidoControllerTest(TestApplicationFactory<Startup> factory)
    {
      _factory = factory;
    }

    [Fact]
    public async Task GetAll()
    {
      // Arrange
      var client = _factory.CreateClient();

      // Act
      var response = await client.GetAsync("/Pedido");

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var list = JsonSerializer.Deserialize<IEnumerable<PedidoViewModel>>(result);
      Assert.Empty(list);
    }
  }
}
