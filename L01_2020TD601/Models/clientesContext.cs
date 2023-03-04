using Microsoft.EntityFrameworkCore;

namespace L01_2020TD601.Models
{
    public class clientesContext : DbContext
    {
        public clientesContext(DbContextOptions<clientesContext> options) : base(options)
        {

        }
        public DbSet<clientes> clientes { get; set; }
    }
}
