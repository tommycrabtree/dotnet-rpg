using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {                        
        }

        // Patrick types the "=> Set<Character>();" in place of the { get; set; } that was
        // automatically included when I created the prop; Either syntax will work just fine
        public DbSet<Character> Characters { get; set; }
    }
}
