using Microsoft.EntityFrameworkCore;
using crud.madals;

namespace crud.data
{
    public class databaseClass:DbContext
    {
        public databaseClass(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Data> Inventory{ get; set; }
    
    
    
    
    
    }
}
