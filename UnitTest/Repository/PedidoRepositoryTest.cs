using API.Repository;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace UnitTest.Repository
{
  public class PedidoRepositoryTest
  {
    [Fact]
    public void GetAll()
    {
      // Arrange
      var config = new Mock<IConfiguration>();
      var repos = new PedidoRepository(config.Object);

      // Act
      var list = repos.GetAll();

      // Assert
      Assert.NotEmpty(list);
    }
  }
}
