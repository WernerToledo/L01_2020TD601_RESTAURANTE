using Microsoft.EntityFrameworkCore;

namespace L01_2020TD601.Models
{
    public class platosContext :DbContext
    {
        public platosContext(DbContextOptions<platosContext> options) : base(options) { }

        public DbSet<platos> platos { get; set; }
    }
}
