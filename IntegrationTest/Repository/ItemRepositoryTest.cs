using API;
using API.Model;
using API.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Repository
{
  public class ItemRepositoryTest : IClassFixture<TestApplicationFactory<Startup>>
  {
    private readonly IServiceScope _serviceScope;
    private IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

    private IItemRepository Repos => ServiceProvider.GetService<IItemRepository>();

    public ItemRepositoryTest(TestApplicationFactory<Startup> factory)
    {
      _serviceScope = factory.Services.CreateScope();
    }

    [Fact]
    public async Task GetAll()
    {
      // Arrange
      var item = new Item() { Descricao = "Descrição" };
      var id = await Repos.Insert(item).ConfigureAwait(false);

      // Act
      var list = await Repos.GetAll().ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(list);
      Repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Get_Null()
    {
      // Act
      var pedido = await Repos.Get(0).ConfigureAwait(false);

      // Assert
      Assert.Null(pedido);
    }

    [Fact]
    public async Task Get()
    {
      // Arrange
      var pedido = new Item() { Descricao = "Descrição" };
      var id = await Repos.Insert(pedido).ConfigureAwait(false);

      // Act
      pedido = await Repos.Get(id).ConfigureAwait(false);

      // Assert
      Assert.NotNull(pedido);
      Repos.Delete(id).Wait();
    }

    [Fact]
    public async Task GetByPedido_Empty()
    {
      // Act
      var list = await Repos.GetByPedido(1).ConfigureAwait(false);

      // Assert
      Assert.Empty(list);
    }

    [Fact]
    public async Task GetByPedido()
    {
      // Arrange
      var idPedido = new Random().Next(1, int.MaxValue);
      var pedido = new Item() { IdPedido = idPedido, Descricao = "Descrição" };
      var id = await Repos.Insert(pedido).ConfigureAwait(false);

      // Act
      var list = await Repos.GetByPedido(idPedido).ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(list);
      Repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Insert_Null()
    {
      // Arrange
      Item item = null;

      // Act
      var id = await Repos.Insert(item).ConfigureAwait(false);

      // Assert
      Assert.Equal(-1, id);
    }

    [Fact]
    public async Task Insert()
    {
      // Arrange
      var item = new Item() { Descricao = "Descrição" };

      // Act
      var id = await Repos.Insert(item).ConfigureAwait(false);

      // Assert
      Assert.True(id > 0);
      Repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Update_Null()
    {
      // Arrange
      Item item = null;

      // Act
      var updated = await Repos.Update(item).ConfigureAwait(false);

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
      var updated = await Repos.Update(item).ConfigureAwait(false);

      // Assert
      Assert.False(updated);
    }

    [Fact]
    public async Task Update_True()
    {
      // Arrange
      var item = new Item() { Descricao = "Descrição" };
      var id = await Repos.Insert(item).ConfigureAwait(false);

      // Act
      item.Descricao = "Outra";
      var updated = await Repos.Update(item).ConfigureAwait(false);

      // Assert
      Assert.True(updated);
      Repos.Delete(id).Wait();
    }

    [Fact]
    public async Task Delete_0()
    {
      // Act
      var deleted = await Repos.Delete(0).ConfigureAwait(false);

      // Assert
      Assert.False(deleted);
    }

    [Fact]
    public async Task Delete()
    {
      // Arrange
      var item = new Item() { Descricao = "Descrição" };
      var id = await Repos.Insert(item).ConfigureAwait(false);

      // Act
      var deleted = await Repos.Delete(id).ConfigureAwait(false);

      // Assert
      Assert.True(deleted);
    }

    [Fact]
    public async Task DeleteByPedido_Empty()
    {
      // Act
      var deleted = await Repos.DeleteByPedido(1).ConfigureAwait(false);

      // Assert
      Assert.False(deleted);
    }

    [Fact]
    public async Task DeleteByPedido()
    {
      // Arrange
      var id = new Random().Next(1, int.MaxValue);
      var item = new Item() { IdPedido = id, Descricao = "Descrição" };
      _ = await Repos.Insert(item).ConfigureAwait(false);

      // Act
      var deleted = await Repos.DeleteByPedido(item.IdPedido).ConfigureAwait(false);

      // Assert
      Assert.True(deleted);
    }
  }
}
