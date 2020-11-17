using API.Connection;
using API.Model;
using API.Repository;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Repository
{
  public class PedidoRepositoryTest
  {
    private readonly IDbCnn _cnn;

    public PedidoRepositoryTest()
    {
      _cnn = Cnn.GetCnn(null);
    }

    [Fact]
    public async Task GetAll_Empty()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);

      // Act
      var list = await repos.GetAll().ConfigureAwait(false);

      // Assert
      Assert.Empty(list);
    }

    [Fact]
    public async Task GetAll()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);
      var pedido = new Pedido() { Status = "Aguardando" };
      var id = await repos.Insert(pedido).ConfigureAwait(false);

      // Act
      var list = await repos.GetAll().ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(list);
      repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Get_Null()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);

      // Act
      var pedido = await repos.Get(0).ConfigureAwait(false);

      // Assert
      Assert.Null(pedido);
    }

    [Fact]
    public async Task Get()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);
      var pedido = new Pedido() { Status = "Aguardando" };
      var id = await repos.Insert(pedido).ConfigureAwait(false);

      // Act
      pedido = await repos.Get(id).ConfigureAwait(false);

      // Assert
      Assert.NotNull(pedido);
      repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Insert_Null()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);
      Pedido pedido = null;

      // Act
      var id = await repos.Insert(pedido).ConfigureAwait(false);

      // Assert
      Assert.Equal(-1, id);
    }

    [Fact]
    public async Task Insert()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);
      var pedido = new Pedido() { Status = "Aguardando" };

      // Act
      var id = await repos.Insert(pedido).ConfigureAwait(false);

      // Assert
      Assert.True(id > 0);
      repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Update_Null()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);
      Pedido pedido = null;

      // Act
      var updated = await repos.Update(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(updated);
    }

    [Fact]
    public async Task Update_False()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);
      var pedido = new Pedido() { Status = "Aguardando" };

      // Act
      pedido.Status = "Aprovado";
      var updated = await repos.Update(pedido).ConfigureAwait(false);

      // Assert
      Assert.False(updated);
    }

    [Fact]
    public async Task Update_True()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);
      var pedido = new Pedido() { Status = "Aguardando" };
      var id = await repos.Insert(pedido).ConfigureAwait(false);

      // Act
      pedido.Status = "Aprovado";
      var updated = await repos.Update(pedido).ConfigureAwait(false);

      // Assert
      Assert.True(updated);
      repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Delete_0()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);

      // Act
      var deleted = await repos.Delete(0).ConfigureAwait(false);

      // Assert
      Assert.False(deleted);
    }

    [Fact]
    public async Task Delete()
    {
      // Arrange
      var repos = new PedidoRepository(_cnn);
      var pedido = new Pedido() { Status = "Aguardando" };
      var id = await repos.Insert(pedido).ConfigureAwait(false);

      // Act
      var deleted = await repos.Delete(id).ConfigureAwait(false);

      // Assert
      Assert.True(deleted);
    }
  }
}
