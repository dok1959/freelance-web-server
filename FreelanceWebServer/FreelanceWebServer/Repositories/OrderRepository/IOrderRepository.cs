using System.Threading.Tasks;
using System.Collections.Generic;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Repositories
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> Get(long id);
        Task Update(Order order);
        Task Delete(long id);
    }
}
