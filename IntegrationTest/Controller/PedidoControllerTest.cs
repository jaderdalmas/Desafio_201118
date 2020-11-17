using API;
using API.Extension;
using API.ViewModel;
using System;
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
    private PedidoViewModel Pedido => new PedidoViewModel()
    {
      Numero = new Random().Next(0, int.MaxValue).ToString(),
      Itens = new List<ItemViewModel>() {
        new ItemViewModel(){
          Descricao = "Item A",
          PrecoUnitario = 10,
          Quantidade = 1
        },
        new ItemViewModel(){
          Descricao = "Item B",
          PrecoUnitario = 5,
          Quantidade = 2
        }
      }
    };

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
      var response = await client.GetAsync("api/Pedido/GetAll");

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var list = JsonSerializer.Deserialize<IEnumerable<PedidoViewModel>>(result);
      Assert.Empty(list);
    }

    [Fact]
    public async Task Get_Empty()
    {
      // Arrange
      var client = _factory.CreateClient();

      // Act
      var response = await client.GetAsync($"api/Pedido/1");

      // Assert
      Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.Empty(result);
    }

    [Fact]
    public async Task Get()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = Pedido;

      // Act
      client.PostAsync($"api/Pedido", pedido.AsContent()).Wait();
      var response = await client.GetAsync($"api/Pedido/{pedido.Numero}");

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<PedidoViewModel>(result);
      Assert.NotNull(item);
      Assert.NotEmpty(item.Itens);

      // Clean
      client.DeleteAsync($"api/Pedido/{pedido.Numero}").Wait();
    }

    [Fact]
    public async Task Post_Empty()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = new PedidoViewModel();

      // Act
      var response = await client.PostAsync("api/Pedido", pedido.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<bool>(result);
      Assert.False(item);
    }

    [Fact]
    public async Task Post()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = Pedido;

      // Act
      var response = await client.PostAsync("api/Pedido", pedido.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<bool>(result);
      Assert.True(item);

      // Clean
      client.DeleteAsync($"api/Pedido/{pedido.Numero}").Wait();
    }

    [Fact]
    public async Task Put_Empty()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = new PedidoViewModel();

      // Act
      var response = await client.PutAsync("api/Pedido", pedido.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<bool>(result);
      Assert.False(item);
    }

    [Fact]
    public async Task Put()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = Pedido;

      // Act
      var response = await client.PutAsync("api/Pedido", pedido.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<bool>(result);
      Assert.True(item);

      // Clean
      client.DeleteAsync($"api/Pedido/{pedido.Numero}").Wait();
    }

    [Fact]
    public async Task Delete_False()
    {
      // Arrange
      var client = _factory.CreateClient();

      // Act
      var response = await client.DeleteAsync($"api/Pedido/1");

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<bool>(result);
      Assert.False(item);
    }

    [Fact]
    public async Task Delete_True()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = Pedido;

      // Act
      client.PostAsync($"api/Pedido", pedido.AsContent()).Wait();
      var response = await client.DeleteAsync($"api/Pedido/{pedido.Numero}");

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<bool>(result);
      Assert.True(item);
    }
  }
}
