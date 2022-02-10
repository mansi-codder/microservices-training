using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Entities;
using System;
using System.Linq;

namespace Ordering.Infrastructure.Persistence
{
    public class DataGenerator
    {
        public static Order neworder = new Order
        {
            Id = 1,
            UserName = "User1",
            TotalPrice = 10
        };
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OrderContext(
                serviceProvider.GetRequiredService<DbContextOptions<OrderContext>>()))
            {
                // Look for any board games already in database.
                if (context.Orders.Any())
                {
                    return;   // Database has been seeded
                }
                context.Orders.Add(neworder);

                context.SaveChanges();
            }
        }
    }
}
