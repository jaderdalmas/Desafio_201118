using API.Connection;
using API.Model;
using API.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Repository
{
  public class ItemRepositoryTest
  {
    private readonly IDbCnn _cnn;

    public ItemRepositoryTest()
    {
      _cnn = Cnn.GetCnn(null);
    }

    [Fact]
    public async Task GetAll_Empty()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);

      // Act
      var list = await repos.GetAll().ConfigureAwait(false);

      // Assert
      Assert.Empty(list);
    }

    [Fact]
    public async Task GetAll()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);
      var item = new Item() { Descricao = "Descrição" };
      var id = await repos.Insert(item).ConfigureAwait(false);

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
      var repos = new ItemRepository(_cnn);

      // Act
      var pedido = await repos.Get(0).ConfigureAwait(false);

      // Assert
      Assert.Null(pedido);
    }

    [Fact]
    public async Task Get()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);
      var pedido = new Item() { Descricao = "Descrição" };
      var id = await repos.Insert(pedido).ConfigureAwait(false);

      // Act
      pedido = await repos.Get(id).ConfigureAwait(false);

      // Assert
      Assert.NotNull(pedido);
      repos.Delete(id).Wait();
    }

    [Fact]
    public async Task GetByPedido_Empty()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);

      // Act
      var list = await repos.GetByPedido(1).ConfigureAwait(false);

      // Assert
      Assert.Empty(list);
    }

    [Fact]
    public async Task GetByPedido()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);
      var idPedido = new Random().Next(1, int.MaxValue);
      var pedido = new Item() { IdPedido = idPedido, Descricao = "Descrição" };
      var id = await repos.Insert(pedido).ConfigureAwait(false);

      // Act
      var list = await repos.GetByPedido(idPedido).ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(list);
      repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Insert_Null()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);
      Item item = null;

      // Act
      var id = await repos.Insert(item).ConfigureAwait(false);

      // Assert
      Assert.Equal(-1, id);
    }

    [Fact]
    public async Task Insert()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);
      var item = new Item() { Descricao = "Descrição" };

      // Act
      var id = await repos.Insert(item).ConfigureAwait(false);

      // Assert
      Assert.True(id > 0);
      repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Update_Null()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);
      Item item = null;

      // Act
      var updated = await repos.Update(item).ConfigureAwait(false);

      // Assert
      Assert.False(updated);
    }

    [Fact]
    public async Task Update_False()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);
      var item = new Item() { Descricao = "Descrição" };

      // Act
      item.Descricao = "Outra";
      var updated = await repos.Update(item).ConfigureAwait(false);

      // Assert
      Assert.False(updated);
    }

    [Fact]
    public async Task Update_True()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);
      var item = new Item() { Descricao = "Descrição" };
      var id = await repos.Insert(item).ConfigureAwait(false);

      // Act
      item.Descricao = "Outra";
      var updated = await repos.Update(item).ConfigureAwait(false);

      // Assert
      Assert.True(updated);
      repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Delete_0()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);

      // Act
      var deleted = await repos.Delete(0).ConfigureAwait(false);

      // Assert
      Assert.False(deleted);
    }

    [Fact]
    public async Task Delete()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);
      var item = new Item() { Descricao = "Descrição" };
      var id = await repos.Insert(item).ConfigureAwait(false);

      // Act
      var deleted = await repos.Delete(id).ConfigureAwait(false);

      // Assert
      Assert.True(deleted);
    }

    [Fact]
    public async Task DeleteByPedido_Empty()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);

      // Act
      var deleted = await repos.DeleteByPedido(1).ConfigureAwait(false);

      // Assert
      Assert.False(deleted);
    }

    [Fact]
    public async Task DeleteByPedido()
    {
      // Arrange
      var repos = new ItemRepository(_cnn);
      var id = new Random().Next(1, int.MaxValue);
      var item = new Item() { IdPedido = id, Descricao = "Descrição" };
      _ = await repos.Insert(item).ConfigureAwait(false);

      // Act
      var deleted = await repos.DeleteByPedido(item.IdPedido).ConfigureAwait(false);

      // Assert
      Assert.True(deleted);
    }
  }
}
