using Microsoft.EntityFrameworkCore;

namespace TheWall.Models{
    public class WallContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WallContext(DbContextOptions<WallContext> options) : base(options) { }
        public DbSet<messages> messages{get;set;}

        public DbSet<users> users{get;set;}
        public DbSet<comments> comments{get;set;}
        
    }
}
