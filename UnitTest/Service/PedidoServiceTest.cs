using API.Model;
using API.Repository;
using API.Service;
using API.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Service
{
  public class PedidoServiceTest
  {
    [Fact]
    public async Task GetAll_Empty()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      var service = new PedidoService(repos.Object);

      // Act
      var list = await service.GetAll().ConfigureAwait(false);

      // Assert
      Assert.Empty(list);
    }

    [Fact]
    public async Task GetAll()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      repos.Setup(x => x.GetAll()).ReturnsAsync(new List<Item>() { new Item() });
      var service = new PedidoService(repos.Object);

      // Act
      var list = await service.GetAll().ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(list);
    }

    [Fact]
    public async Task Get_Null()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      var service = new PedidoService(repos.Object);

      // Act
      var list = await service.Get(0).ConfigureAwait(false);

      // Assert
      Assert.Null(list);
    }

    [Fact]
    public async Task Get()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      repos.Setup(x => x.GetByPedido(It.IsAny<int>())).ReturnsAsync(new List<Item>() { new Item() });
      var service = new PedidoService(repos.Object);

      // Act
      var list = await service.Get(1).ConfigureAwait(false);

      // Assert
      Assert.NotNull(list);
    }

    [Fact]
    public async Task InsertUpdate_Null()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      var service = new PedidoService(repos.Object);
      PedidoViewModel pedido = null;

      // Act
      var done = await service.InsertUpdate(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(done);
    }

    [Fact]
    public async Task InsertUpdate_Empty()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      var service = new PedidoService(repos.Object);
      var pedido = new PedidoViewModel();

      // Act
      var done = await service.InsertUpdate(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(done);
    }

    [Fact]
    public async Task InsertUpdate_Invalid()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      var service = new PedidoService(repos.Object);
      var pedido = new PedidoViewModel() { Numero = "x", Itens = new List<ItemViewModel>() { new ItemViewModel() } };

      // Act
      var done = await service.InsertUpdate(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(done);
    }

    [Fact]
    public async Task InsertUpdate()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      repos.Setup(x => x.Insert(It.IsAny<Item>())).ReturnsAsync(1);
      var service = new PedidoService(repos.Object);

      var idPedido = new Random().Next(1, int.MaxValue);
      var pedido = new PedidoViewModel()
      {
        Numero = idPedido.ToString(),
        Itens = new List<ItemViewModel>() { new ItemViewModel() }
      };

      // Act
      var done = await service.InsertUpdate(pedido).ConfigureAwait(false);

      // Assert
      Assert.True(done);
    }

    [Fact]
    public async Task Delete_0()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      var service = new PedidoService(repos.Object);

      // Act
      var deleted = await service.Delete(0).ConfigureAwait(false);

      // Assert
      Assert.False(deleted);
    }

    [Fact]
    public async Task Delete()
    {
      // Arrange
      var repos = new Mock<IItemRepository>();
      repos.Setup(x => x.DeleteByPedido(It.IsAny<int>())).ReturnsAsync(true);
      var service = new PedidoService(repos.Object);

      // Act
      var deleted = await service.Delete(1).ConfigureAwait(false);

      // Assert
      Assert.True(deleted);
    }
  }
}
