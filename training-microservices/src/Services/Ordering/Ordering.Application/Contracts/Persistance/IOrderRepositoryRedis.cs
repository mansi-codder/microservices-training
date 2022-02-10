using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistance
{
    public interface IOrderRepositoryRedis
    {
        Task<Order> GetOrdersByUserName(string userName);
        Task<Order> UpdateAsync(Order order);
        Task<Order> AddAsync(Order order);
        Task DeleteAsync(string userName);
    }
}
