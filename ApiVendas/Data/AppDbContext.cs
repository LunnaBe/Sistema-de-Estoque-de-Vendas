using Microsoft.EntityFrameworkCore;
using Shared;

namespace ApiVendas.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EstoqueData> Estoque { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


    }
}
