using Microsoft.EntityFrameworkCore;

namespace L01_2020TD601.Models
{
    public class pedidosContext : DbContext
    {
        public pedidosContext(DbContextOptions<pedidosContext> options) : base(options)
        {

        }
        public DbSet<pedidos> pedidos { get; set; }
    }
}
