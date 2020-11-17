using API.Controllers;
using API.Service;
using API.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Controller
{
  public class PedidoControllerTest
  {
    [Fact]
    public async Task GetAll_Empty()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      var cntrlr = new PedidoController(null, service.Object);

      // Act
      var list = await cntrlr.GetAll().ConfigureAwait(false);

      // Assert
      Assert.Empty(list);
    }

    [Fact]
    public async Task GetAll()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      service.Setup(x => x.GetAll()).ReturnsAsync(new List<PedidoViewModel>() { new PedidoViewModel() });
      var cntrlr = new PedidoController(null, service.Object);

      // Act
      var list = await cntrlr.GetAll().ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(list);
    }

    [Fact]
    public async Task Get_Null()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      var cntrlr = new PedidoController(null, service.Object);

      // Act
      var list = await cntrlr.Get(0).ConfigureAwait(false);

      // Assert
      Assert.Null(list);
    }

    [Fact]
    public async Task Get()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      service.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(new PedidoViewModel());
      var cntrlr = new PedidoController(null, service.Object);

      // Act
      var list = await cntrlr.Get(1).ConfigureAwait(false);

      // Assert
      Assert.NotNull(list);
    }

    [Fact]
    public async Task Insert_Null()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      var cntrlr = new PedidoController(null, service.Object);
      PedidoViewModel pedido = null;

      // Act
      var done = await cntrlr.Post(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(done);
    }

    [Fact]
    public async Task Insert_Empty()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      var cntrlr = new PedidoController(null, service.Object);
      var pedido = new PedidoViewModel();

      // Act
      var done = await cntrlr.Post(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(done);
    }

    [Fact]
    public async Task Insert_Invalid()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      var cntrlr = new PedidoController(null, service.Object);
      var pedido = new PedidoViewModel() { Numero = "x", Itens = new List<ItemViewModel>() { new ItemViewModel() } };

      // Act
      var done = await cntrlr.Post(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(done);
    }

    [Fact]
    public async Task Insert()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      service.Setup(x => x.InsertUpdate(It.IsAny<PedidoViewModel>())).ReturnsAsync(true);
      var cntrlr = new PedidoController(null, service.Object);

      var idPedido = new Random().Next(1, int.MaxValue);
      var pedido = new PedidoViewModel()
      {
        Numero = idPedido.ToString(),
        Itens = new List<ItemViewModel>() { new ItemViewModel() }
      };

      // Act
      var done = await cntrlr.Post(pedido).ConfigureAwait(false);

      // Assert
      Assert.True(done);
    }

    [Fact]
    public async Task Update_Null()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      var cntrlr = new PedidoController(null, service.Object);
      PedidoViewModel pedido = null;

      // Act
      var done = await cntrlr.Put(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(done);
    }

    [Fact]
    public async Task Update_Empty()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      var cntrlr = new PedidoController(null, service.Object);
      var pedido = new PedidoViewModel();

      // Act
      var done = await cntrlr.Put(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(done);
    }

    [Fact]
    public async Task Update_Invalid()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      var cntrlr = new PedidoController(null, service.Object);
      var pedido = new PedidoViewModel() { Numero = "x", Itens = new List<ItemViewModel>() { new ItemViewModel() } };

      // Act
      var done = await cntrlr.Put(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(done);
    }

    [Fact]
    public async Task Update()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      service.Setup(x => x.InsertUpdate(It.IsAny<PedidoViewModel>())).ReturnsAsync(true);
      var cntrlr = new PedidoController(null, service.Object);

      var idPedido = new Random().Next(1, int.MaxValue);
      var pedido = new PedidoViewModel()
      {
        Numero = idPedido.ToString(),
        Itens = new List<ItemViewModel>() { new ItemViewModel() }
      };

      // Act
      var done = await cntrlr.Put(pedido).ConfigureAwait(false);

      // Assert
      Assert.True(done);
    }

    [Fact]
    public async Task Delete_0()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      var cntrlr = new PedidoController(null, service.Object);

      // Act
      var deleted = await cntrlr.Delete(0).ConfigureAwait(false);

      // Assert
      Assert.False(deleted);
    }

    [Fact]
    public async Task Delete()
    {
      // Arrange
      var service = new Mock<IPedidoService>();
      service.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(true);
      var cntrlr = new PedidoController(null, service.Object);

      // Act
      var deleted = await cntrlr.Delete(1).ConfigureAwait(false);

      // Assert
      Assert.True(deleted);
    }
  }
}
