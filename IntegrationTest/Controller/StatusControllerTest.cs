using API;
using API.Extension;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Controller
{
  public class StatusControllerTest : IClassFixture<TestApplicationFactory<Startup>>
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

    public StatusControllerTest(TestApplicationFactory<Startup> factory)
    {
      _factory = factory;
    }

    [Fact]
    public async Task Post_BadRequest()
    {
      // Arrange
      var client = _factory.CreateClient();
      var status = new StatusRequest();

      // Act
      var response = await client.PostAsync("api/Status", status.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<ProblemDetails>(result);
      Assert.NotEmpty(item.Title);
      Assert.NotEmpty(item.Type);
      Assert.NotEmpty(item.Extensions);
    }

    [Fact]
    public async Task Post_Empty()
    {
      // Arrange
      var client = _factory.CreateClient();
      var status = new StatusRequest()
      {
        Pedido = new Random().Next(1, int.MaxValue).ToString(),
        ItensAprovados = 2,
        ValorAprovado = 20,
        Status = "x"
      };

      // Act
      var response = await client.PostAsync("api/Status", status.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<StatusResponse>(result);
      Assert.NotNull(item);
      Assert.NotNull(item.Pedido);
      Assert.NotEmpty(item.Status);
      Assert.Equal("CODIGO_PEDIDO_INVALIDO", item.Status.First());
    }

    [Fact]
    public async Task Post_Reprovado()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = Pedido;
      var status = new StatusRequest()
      {
        Pedido = pedido.Numero,
        ItensAprovados = 2,
        ValorAprovado = 20,
        Status = "Reprovado"
      };

      // Act
      client.PostAsync("api/Pedido", pedido.AsContent()).Wait();
      var response = await client.PostAsync("api/Status", status.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<StatusResponse>(result);
      Assert.NotNull(item);
      Assert.NotNull(item.Pedido);
      Assert.NotEmpty(item.Status);
      Assert.Equal("REPROVADO", item.Status.First());
    }

    [Fact]
    public async Task Post_Aprovado()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = Pedido;
      var status = new StatusRequest()
      {
        Pedido = pedido.Numero,
        ItensAprovados = 2,
        ValorAprovado = 20,
        Status = "Aprovado"
      };

      // Act
      client.PostAsync("api/Pedido", pedido.AsContent()).Wait();
      var response = await client.PostAsync("api/Status", status.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<StatusResponse>(result);
      Assert.NotNull(item);
      Assert.NotNull(item.Pedido);
      Assert.NotEmpty(item.Status);
      Assert.Equal("APROVADO", item.Status.First());
    }

    [Fact]
    public async Task Post_Aprovado_ValorMenor()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = Pedido;
      var status = new StatusRequest()
      {
        Pedido = pedido.Numero,
        ItensAprovados = 2,
        ValorAprovado = 10,
        Status = "Aprovado"
      };

      // Act
      client.PostAsync("api/Pedido", pedido.AsContent()).Wait();
      var response = await client.PostAsync("api/Status", status.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<StatusResponse>(result);
      Assert.NotNull(item);
      Assert.NotNull(item.Pedido);
      Assert.NotEmpty(item.Status);
      Assert.Equal("APROVADO_VALOR_A_MENOR", item.Status.First());
    }

    [Fact]
    public async Task Post_Aprovado_QtdMenor()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = Pedido;
      var status = new StatusRequest()
      {
        Pedido = pedido.Numero,
        ItensAprovados = 1,
        ValorAprovado = 20,
        Status = "Aprovado"
      };

      // Act
      client.PostAsync("api/Pedido", pedido.AsContent()).Wait();
      var response = await client.PostAsync("api/Status", status.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<StatusResponse>(result);
      Assert.NotNull(item);
      Assert.NotNull(item.Pedido);
      Assert.NotEmpty(item.Status);
      Assert.Equal("APROVADO_QTD_A_MENOR", item.Status.First());
    }

    [Fact]
    public async Task Post_Aprovado_Maior()
    {
      // Arrange
      var client = _factory.CreateClient();
      var pedido = Pedido;
      var status = new StatusRequest()
      {
        Pedido = pedido.Numero,
        ItensAprovados = 3,
        ValorAprovado = 30,
        Status = "Aprovado"
      };

      // Act
      client.PostAsync("api/Pedido", pedido.AsContent()).Wait();
      var response = await client.PostAsync("api/Status", status.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<StatusResponse>(result);
      Assert.NotNull(item);
      Assert.NotNull(item.Pedido);
      Assert.NotEmpty(item.Status);
      Assert.Equal("APROVADO_QTD_A_MAIOR", item.Status[0]);
      Assert.Equal("APROVADO_VALOR_A_MAIOR", item.Status[1]);
    }
  }
}
