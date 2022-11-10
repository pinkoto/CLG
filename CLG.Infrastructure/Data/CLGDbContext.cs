using CLG.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CLG.Infrastructure.Data
{
    public class CLGDbContext : DbContext
    {
        public CLGDbContext(DbContextOptions<CLGDbContext> options) : base(options)
        {
            
        }

        public DbSet<Credentials> AllCredentials { get; set; }
    }
}
