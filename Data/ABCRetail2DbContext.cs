using Microsoft.EntityFrameworkCore;
using ABC_Retail2.Models;

namespace ABC_Retail2.Data
{
    public class ABCRetail2DbContext : DbContext
    {
        public ABCRetail2DbContext(DbContextOptions<ABCRetail2DbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
