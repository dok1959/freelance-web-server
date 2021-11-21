using System;
using System.Collections.Generic;
using System.Linq;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Repositories
{
    public class MemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();
        private long _idCounter = 1;

        public void Add(Order order)
        {
            order.Id = _idCounter++;
            _orders.Add(order);
        }

        public IEnumerable<Order> GetAll() => _orders.ToList();

        public Order GetById(long id) => _orders.Find(o => o.Id.Equals(id));

        public IEnumerable<Order> FindAll(Func<Order, bool> predicate) => _orders.Where(predicate);

        public void Update(Order order)
        {
            _orders.RemoveAll(o => o.Id.Equals(order.Id));
            _orders.Add(order);
        }

        public void DeleteById(long id) => _orders.RemoveAll(o => o.Id.Equals(id));
    }
}
