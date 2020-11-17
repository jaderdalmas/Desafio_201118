using API.Model;
using System.Collections.Generic;

namespace API.Repository
{
  public interface IPedidoRepository
  {
    IEnumerable<Pedido> GetAll();
  }
}
