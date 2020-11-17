using API.Connection;
using API.Extension;
using API.Model;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
  public class ItemRepository : IItemRepository
  {
    private readonly IDbCnn _cnn;
    private string TableName => $"{nameof(Item)}s";

    public ItemRepository(IDbCnn cnn)
    {
      _cnn = cnn;

      if (!_cnn.HasTable(TableName))
        CreateTable();
    }

    private void CreateTable()
    {
      var cnn = _cnn.GetConnection();
      cnn.Execute($"CREATE TABLE [{_cnn.Schema}].[{TableName}]([Id][int] NOT NULL IDENTITY(1, 1) PRIMARY KEY ,[IdPedido][int] NOT NULL ,[Descricao][nchar](100) NULL ,[PrecoUnitario][money] NOT NULL ,[Quantidade][int] NOT NULL)");
    }

    public async Task<IEnumerable<Item>> GetAll() => await _cnn.GetConnection().GetAllAsync<Item>().ConfigureAwait(false);

    public async Task<IEnumerable<Item>> GetByPedido(int idPedido)
    {
      if (idPedido < 1)
        return null;

      return await _cnn.GetConnection().QueryAsync<Item>($"select * from {TableName} where IdPedido = {idPedido}").ConfigureAwait(false);
    }

    public async Task<Item> Get(int id)
    {
      if (id < 1)
        return null;

      return await _cnn.GetConnection().GetAsync<Item>(id).ConfigureAwait(false);
    }

    public async Task<int> Insert(Item item)
    {
      if (item is null || !item.IsValid())
        return -1;

      return await _cnn.GetConnection().InsertAsync(item).ConfigureAwait(false);
    }

    public async Task<bool> Update(Item item)
    {
      if (item is null || !item.IsValid())
        return false;

      return await _cnn.GetConnection().UpdateAsync(item).ConfigureAwait(false);
    }

    public async Task<bool> Delete(int id)
    {
      if (id < 1)
        return false;

      return await _cnn.GetConnection().DeleteAsync(new Item() { Id = id }).ConfigureAwait(false);
    }

    public async Task<bool> DeleteByPedido(int idPedido)
    {
      if (idPedido < 1)
        return false;

      return await _cnn.GetConnection().ExecuteAsync($"delete {TableName} where idPedido = {idPedido}").ConfigureAwait(false) > 0;
    }
  }
}
