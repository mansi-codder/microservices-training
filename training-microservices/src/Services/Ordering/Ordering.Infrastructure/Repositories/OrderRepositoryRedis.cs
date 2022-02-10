using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepositoryRedis : IOrderRepositoryRedis
    {
        private readonly IDistributedCache _redisCache;

        public OrderRepositoryRedis(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<Order> GetOrdersByUserName(string userName)
        {
            var order = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(order))
                return null;

            return JsonConvert.DeserializeObject<Order>(order);
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            await _redisCache.SetStringAsync(order.UserName, JsonConvert.SerializeObject(order));

            return await GetOrdersByUserName(order.UserName);
        }

        public async Task<Order> AddAsync(Order order)
        {
            await _redisCache.SetStringAsync(order.UserName, JsonConvert.SerializeObject(order));

            return await GetOrdersByUserName(order.UserName);
        }

        public async Task DeleteAsync(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}