using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Data
{
    public class HRSystemDbContext : DbContext
    {
        public HRSystemDbContext(DbContextOptions<HRSystemDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Project)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
