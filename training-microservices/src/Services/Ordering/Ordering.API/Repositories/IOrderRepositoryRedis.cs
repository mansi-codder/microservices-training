using Ordering.API.Entities;
using System.Threading.Tasks;

namespace Ordering.API.Repositories
{
    public interface IOrderRepositoryRedis
    {
        Task<Order> GetOrdersByUserName(string userName);
        Task<Order> UpdateAsync(Order order);
        Task<Order> AddAsync(Order order);
        Task DeleteAsync(string userName);
    }
}
