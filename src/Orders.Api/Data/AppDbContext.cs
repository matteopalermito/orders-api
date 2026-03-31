using Microsoft.EntityFrameworkCore;
using Orders.Api.Models;

namespace Orders.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Order> Orders => Set<Order>();
    }
}
