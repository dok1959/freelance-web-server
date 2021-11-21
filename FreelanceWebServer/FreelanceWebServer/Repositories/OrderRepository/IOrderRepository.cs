using System;
using System.Collections.Generic;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        IEnumerable<Order> GetAll();
        Order GetById(long id);
        IEnumerable<Order> FindAll(Func<Order, bool> predicate);
        void Update(Order order);
        void DeleteById(long id);
    }
}
