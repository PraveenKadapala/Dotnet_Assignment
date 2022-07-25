namespace Project.Models
{
    using Microsoft.EntityFrameworkCore;
    namespace Project.Models
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }
            
            public DbSet<Student> students { get; set; }
            public DbSet<Teachers> teachers  { get; set; }
            public DbSet<Courses> courses { get; set; }
        }
    }
}

