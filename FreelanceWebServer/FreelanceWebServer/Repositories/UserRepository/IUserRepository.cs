using System.Threading.Tasks;
using System.Collections.Generic;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(long id);
        Task<User> GetByUsername(string username);
        Task<User> GetByPhoneNumber(string phoneNumber);
        Task<User> GetByRefreshToken(string token);
        Task Update(User user);
        Task UpdateRefreshToken(long id, string token);
        Task Delete(long id);
    }
}