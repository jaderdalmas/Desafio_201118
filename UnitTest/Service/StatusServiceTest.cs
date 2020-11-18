using API.Model;
using API.Repository;
using API.Service;
using API.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Service
{
  public class StatusServiceTest
  {
    [Fact]
    public async Task Update_Null()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      var service = new StatusService(repos.Object);
      StatusRequest status = null;

      // Act
      var result = await service.Update(status).ConfigureAwait(false);

      // Assert
      Assert.NotNull(result);
      Assert.Null(result.Pedido);
      Assert.NotEmpty(result.Status);
      Assert.Equal("CODIGO_PEDIDO_INVALIDO", result.Status.First());
    }

    [Fact]
    public async Task Update_Empty()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      var service = new StatusService(repos.Object);
      var status = new StatusRequest();

      // Act
      var result = await service.Update(status).ConfigureAwait(false);

      // Assert
      Assert.NotNull(result);
      Assert.Null(result.Pedido);
      Assert.NotEmpty(result.Status);
      Assert.Equal("CODIGO_PEDIDO_INVALIDO", result.Status.First());
    }

    [Fact]
    public async Task Update_Invalid()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      var service = new StatusService(repos.Object);
      var status = new StatusRequest() { Pedido = "x", Status = "x" };

      // Act
      var result = await service.Update(status).ConfigureAwait(false);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(status.Pedido, result.Pedido);
      Assert.NotEmpty(result.Status);
      Assert.Equal("CODIGO_PEDIDO_INVALIDO", result.Status.First());
    }

    [Fact]
    public async Task Update_Reprovado()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      repos.Setup(x => x.GetByPedido(It.IsAny<int>())).ReturnsAsync(new List<Item>() { new Item() });
      var service = new StatusService(repos.Object);

      var idPedido = new Random().Next(1, int.MaxValue);
      var status = new StatusRequest()
      {
        Pedido = new Random().Next(1, int.MaxValue).ToString(),
        ItensAprovados = 1,
        Status = "Reprovado"
      };

      // Act
      var result = await service.Update(status).ConfigureAwait(false);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(status.Pedido, result.Pedido);
      Assert.NotEmpty(result.Status);
      Assert.Equal("REPROVADO", result.Status.First());
    }

    [Fact]
    public async Task Update_Aprovado_Maior()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      repos.Setup(x => x.GetByPedido(It.IsAny<int>())).ReturnsAsync(new List<Item>() { new Item() });
      var service = new StatusService(repos.Object);

      var idPedido = new Random().Next(1, int.MaxValue);
      var status = new StatusRequest()
      {
        Pedido = new Random().Next(1, int.MaxValue).ToString(),
        ItensAprovados = 2,
        ValorAprovado = 1,
        Status = "Aprovado"
      };

      // Act
      var result = await service.Update(status).ConfigureAwait(false);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(status.Pedido, result.Pedido);
      Assert.NotEmpty(result.Status);
      Assert.Equal("APROVADO_QTD_A_MAIOR", result.Status[0]);
      Assert.Equal("APROVADO_VALOR_A_MAIOR", result.Status[1]);
    }

    [Fact]
    public async Task Update_Aprovado_Menor()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      repos.Setup(x => x.GetByPedido(It.IsAny<int>())).ReturnsAsync(new List<Item>() { new Item(), new Item() { PrecoUnitario = 1, Quantidade = 2 } });
      var service = new StatusService(repos.Object);

      var idPedido = new Random().Next(1, int.MaxValue);
      var status = new StatusRequest()
      {
        Pedido = new Random().Next(1, int.MaxValue).ToString(),
        ItensAprovados = 1,
        ValorAprovado = 1,
        Status = "Aprovado"
      };

      // Act
      var result = await service.Update(status).ConfigureAwait(false);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(status.Pedido, result.Pedido);
      Assert.NotEmpty(result.Status);
      Assert.Equal("APROVADO_QTD_A_MENOR", result.Status[0]);
      Assert.Equal("APROVADO_VALOR_A_MENOR", result.Status[1]);
    }

    [Fact]
    public async Task Update_Aprovado()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      repos.Setup(x => x.GetByPedido(It.IsAny<int>())).ReturnsAsync(new List<Item>() { new Item() });
      var service = new StatusService(repos.Object);

      var idPedido = new Random().Next(1, int.MaxValue);
      var status = new StatusRequest()
      {
        Pedido = new Random().Next(1, int.MaxValue).ToString(),
        ItensAprovados = 1,
        Status = "Aprovado"
      };

      // Act
      var result = await service.Update(status).ConfigureAwait(false);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(status.Pedido, result.Pedido);
      Assert.NotEmpty(result.Status);
      Assert.Equal("APROVADO", result.Status.First());
    }
  }
}
