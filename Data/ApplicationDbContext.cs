using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewspaperCMS.Models;

namespace NewspaperCMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<NewspaperCMS.Models.article>? article { get; set; }
        public DbSet<NewspaperCMS.Models.category>? category { get; set; }
    }
}