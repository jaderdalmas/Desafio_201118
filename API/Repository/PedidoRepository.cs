using API.Model;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace API.Repository
{
  public class PedidoRepository : IPedidoRepository
  {
    private IConfiguration _config { get; set; }

    public PedidoRepository(IConfiguration config)
    {
      _config = config;
    }

    public IEnumerable<Pedido> GetAll()
    {
      using var cnn = new SqlConnection(_config.GetConnectionString("Pedido"));

      return cnn.GetAll<Pedido>();
    }
  }
}
