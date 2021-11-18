using FreelanceWebServer.Models;
using System.Collections.Generic;

namespace FreelanceWebServer.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        void Add(T item);

        IEnumerable<T> GetAll();

        T GetById(long id);

        IEnumerable<T> Where();

        void Update(T item);

        void Delete(T item);
    }
}
