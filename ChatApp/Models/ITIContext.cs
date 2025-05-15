using Microsoft.EntityFrameworkCore;

namespace ChatApp.Models
{
    public class ITIContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        //cto Option
        public ITIContext(DbContextOptions<ITIContext> options):base(options)
        {
            
        }
    }
}
