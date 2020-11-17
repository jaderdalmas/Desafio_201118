using API;
using API.Model;
using API.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Repository
{
  public class ItemRepositoryTest : IClassFixture<TestApplicationFactory<Startup>>
  {
    private IItemRepository _repos;

    public ItemRepositoryTest(IItemRepository itemRepository)
    {
      _repos = itemRepository;
    }

    [Fact]
    public async Task GetAll_Empty()
    {
      // Act
      var list = await _repos.GetAll().ConfigureAwait(false);

      // Assert
      Assert.Empty(list);
    }

    [Fact]
    public async Task GetAll()
    {
      // Arrange
      var item = new Item() { Descricao = "Descrição" };
      var id = await _repos.Insert(item).ConfigureAwait(false);

      // Act
      var list = await _repos.GetAll().ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(list);
      _repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Get_Null()
    {
      // Act
      var pedido = await _repos.Get(0).ConfigureAwait(false);

      // Assert
      Assert.Null(pedido);
    }

    [Fact]
    public async Task Get()
    {
      // Arrange
      var pedido = new Item() { Descricao = "Descrição" };
      var id = await _repos.Insert(pedido).ConfigureAwait(false);

      // Act
      pedido = await _repos.Get(id).ConfigureAwait(false);

      // Assert
      Assert.NotNull(pedido);
      _repos.Delete(id).Wait();
    }

    [Fact]
    public async Task GetByPedido_Empty()
    {
      // Act
      var list = await _repos.GetByPedido(1).ConfigureAwait(false);

      // Assert
      Assert.Empty(list);
    }

    [Fact]
    public async Task GetByPedido()
    {
      // Arrange
      var idPedido = new Random().Next(1, int.MaxValue);
      var pedido = new Item() { IdPedido = idPedido, Descricao = "Descrição" };
      var id = await _repos.Insert(pedido).ConfigureAwait(false);

      // Act
      var list = await _repos.GetByPedido(idPedido).ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(list);
      _repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Insert_Null()
    {
      // Arrange
      Item item = null;

      // Act
      var id = await _repos.Insert(item).ConfigureAwait(false);

      // Assert
      Assert.Equal(-1, id);
    }

    [Fact]
    public async Task Insert()
    {
      // Arrange
      var item = new Item() { Descricao = "Descrição" };

      // Act
      var id = await _repos.Insert(item).ConfigureAwait(false);

      // Assert
      Assert.True(id > 0);
      _repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Update_Null()
    {
      // Arrange
      Item item = null;

      // Act
      var updated = await _repos.Update(item).ConfigureAwait(false);

      // Assert
      Assert.False(updated);
    }

    [Fact]
    public async Task Update_False()
    {
      // Arrange
      var item = new Item() { Descricao = "Descrição" };

      // Act
      item.Descricao = "Outra";
      var updated = await _repos.Update(item).ConfigureAwait(false);

      // Assert
      Assert.False(updated);
    }

    [Fact]
    public async Task Update_True()
    {
      // Arrange
      var item = new Item() { Descricao = "Descrição" };
      var id = await _repos.Insert(item).ConfigureAwait(false);

      // Act
      item.Descricao = "Outra";
      var updated = await _repos.Update(item).ConfigureAwait(false);

      // Assert
      Assert.True(updated);
      _repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Delete_0()
    {
      // Act
      var deleted = await _repos.Delete(0).ConfigureAwait(false);

      // Assert
      Assert.False(deleted);
    }

    [Fact]
    public async Task Delete()
    {
      // Arrange
      var item = new Item() { Descricao = "Descrição" };
      var id = await _repos.Insert(item).ConfigureAwait(false);

      // Act
      var deleted = await _repos.Delete(id).ConfigureAwait(false);

      // Assert
      Assert.True(deleted);
    }

    [Fact]
    public async Task DeleteByPedido_Empty()
    {
      // Act
      var deleted = await _repos.DeleteByPedido(1).ConfigureAwait(false);

      // Assert
      Assert.False(deleted);
    }

    [Fact]
    public async Task DeleteByPedido()
    {
      // Arrange
      var id = new Random().Next(1, int.MaxValue);
      var item = new Item() { IdPedido = id, Descricao = "Descrição" };
      _ = await _repos.Insert(item).ConfigureAwait(false);

      // Act
      var deleted = await _repos.DeleteByPedido(item.IdPedido).ConfigureAwait(false);

      // Assert
      Assert.True(deleted);
    }
  }
}
