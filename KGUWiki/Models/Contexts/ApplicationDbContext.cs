using Microsoft.EntityFrameworkCore;

namespace KGUWiki.Models.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UniversityEmployee> UniversityEmployees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
