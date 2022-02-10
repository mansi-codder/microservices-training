using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected void OnModelCreating1(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
            new Order() {Id=2, UserName = "first lucky order user", LastName = "hun mai", EmailAddress = "ezozkme@gmail.com" ,PaymentMethod=1 , CardNumber="1234 56789" }
          );
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "Trainee";
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.Now;
                        entry.Entity.ModifiedBy = "Tutor";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
