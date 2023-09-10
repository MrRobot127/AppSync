using ERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.Api.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
        public virtual DbSet<CompanyGroup> CompanyGroup { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
