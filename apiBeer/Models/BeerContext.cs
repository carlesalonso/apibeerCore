using Microsoft.EntityFrameworkCore;
namespace apiBeer.Models
{
    public class BeerContext : DbContext
    {
        public BeerContext(DbContextOptions<BeerContext> options): base(options)
        {
            
        }
        public DbSet<BeerItem> BeerItems { get; set; }
    }
}
