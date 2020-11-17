using API;
using API.Extension;
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
      var response = await client.GetAsync("/Pedido/GetAll");

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var list = JsonSerializer.Deserialize<IEnumerable<PedidoViewModel>>(result);
      Assert.Empty(list);
    }

    [Fact]
    public async Task Get_404()
    {
      // Arrange
      var client = _factory.CreateClient();

      // Act
      var response = await client.GetAsync($"/Pedido/1");

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.Empty(result);
    }

    [Fact]
    public async Task Post_Empty()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = new PedidoViewModel();

      // Act
      var response = await client.PostAsync("/Pedido", pedido.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<bool>(result);
      Assert.False(item);
    }

    [Fact]
    public async Task Put_Empty()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = new PedidoViewModel();

      // Act
      var response = await client.PutAsync("/Pedido", pedido.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<bool>(result);
      Assert.False(item);
    }

    [Fact]
    public async Task Delete_404()
    {
      // Arrange
      var client = _factory.CreateClient();

      // Act
      var response = await client.DeleteAsync($"/Pedido/1");

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.Empty(result);
    }
  }
}
