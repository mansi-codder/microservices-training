using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        // public List<Order> _orderList = new List<Order>();
        private readonly OrderContext _context;

        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Order> GetOrdersByUserName(string userName)
        {
            IEnumerable<Order> orders1= new List<Order>();
            try
            {
                orders1 = _context.Orders.Where(o => o.UserName == userName);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return orders1;
        }
        public Order AddOrder(Order entity)
        {
            entity.Id =  _context.Orders.Select(x => x.Id).Max() + 1;
            _context.Orders.Add(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
