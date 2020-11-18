using API.Controllers;
using API.Service;
using API.ViewModel;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Controller
{
  public class StatusControllerTest
  {
    [Fact]
    public async Task Update_Null()
    {
      // Arrange
      var service = new Mock<IStatusService>();
      var cntrlr = new StatusController(null, service.Object);
      StatusRequest status = null;

      // Act
      var response = await cntrlr.Post(status).ConfigureAwait(false);

      // Assert
      Assert.Null(response);
    }

    [Fact]
    public async Task Update_Empty()
    {
      // Arrange
      var service = new Mock<IStatusService>();
      var cntrlr = new StatusController(null, service.Object);
      var status = new StatusRequest();

      // Act
      var response = await cntrlr.Post(status).ConfigureAwait(false);

      // Assert
      Assert.Null(response);
    }

    [Fact]
    public async Task Update_Invalid()
    {
      // Arrange
      var service = new Mock<IStatusService>();
      var cntrlr = new StatusController(null, service.Object);
      var status = new StatusRequest() { Pedido = "x", Status = "x" };

      // Act
      var response = await cntrlr.Post(status).ConfigureAwait(false);

      // Assert
      Assert.Null(response);
    }

    [Fact]
    public async Task Update()
    {
      // Arrange
      var service = new Mock<IStatusService>();
      service.Setup(x => x.Update(It.IsAny<StatusRequest>())).ReturnsAsync(new StatusResponse());
      var cntrlr = new StatusController(null, service.Object);

      var idPedido = new Random().Next(1, int.MaxValue);
      var pedido = new StatusRequest()
      {
        Pedido = idPedido.ToString(),
        Status = "Aprovado"
      };

      // Act
      var response = await cntrlr.Post(pedido).ConfigureAwait(false);

      // Assert
      Assert.NotNull(response);
    }
  }
}
