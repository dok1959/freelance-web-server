using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Repositories
{
    public class MemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();
        private long _idCounter = 1;

        public Task Add(Order order)
        {
            order.Id = _idCounter++;
            _orders.Add(order);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Order>> GetAll() 
            => await Task.FromResult(_orders.ToList());

        public async Task<Order> Get(long id) 
            => await Task.FromResult(_orders.Find(o => o.Id.Equals(id)));

        public Task Update(Order order)
        {
            _orders.RemoveAll(o => o.Id.Equals(order.Id));
            _orders.Add(order);

            return Task.CompletedTask;
        }

        public async Task Delete(long id) 
            => await Task.FromResult(_orders.RemoveAll(o => o.Id.Equals(id)));
    }
}
