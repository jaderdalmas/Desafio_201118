using API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
  public interface IPedidoRepository
  {
    Task<IEnumerable<Pedido>> GetAll();
  }
}
