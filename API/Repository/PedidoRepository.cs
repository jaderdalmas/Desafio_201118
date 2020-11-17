using API.Connection;
using API.Extension;
using API.Model;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
  public class PedidoRepository : IPedidoRepository
  {
    private readonly IDbCnn _cnn;
    private string TableName => $"{nameof(Pedido)}s";

    public PedidoRepository(IDbCnn cnn)
    {
      _cnn = cnn;

      if (!_cnn.HasTable(TableName))
        CreateTable();
    }

    private void CreateTable()
    {
      var cnn = _cnn.GetConnection();
      cnn.Execute($"CREATE TABLE [{_cnn.Schema}].[{TableName}]([Id][int] NOT NULL IDENTITY(1, 1) PRIMARY KEY ,[Status][varchar](10) NOT NULL)");
    }

    public async Task<IEnumerable<Pedido>> GetAll() => await _cnn.GetConnection().GetAllAsync<Pedido>().ConfigureAwait(false);

    public async Task<Pedido> Get(int id)
    {
      if (id < 1)
        return null;

      return await _cnn.GetConnection().GetAsync<Pedido>(id).ConfigureAwait(false);
    }

    public async Task<int> Insert(Pedido pedido)
    {
      if (pedido is null || !pedido.IsValid())
        return -1;

      return await _cnn.GetConnection().InsertAsync(pedido).ConfigureAwait(false);
    }

    public async Task<bool> Update(Pedido pedido)
    {
      if (pedido is null || !pedido.IsValid())
        return false;

      return await _cnn.GetConnection().UpdateAsync(pedido).ConfigureAwait(false);
    }

    public async Task<bool> Delete(int id)
    {
      if (id < 1)
        return false;

      return await _cnn.GetConnection().DeleteAsync(new Pedido() { Id = id }).ConfigureAwait(false);
    }
  }
}
