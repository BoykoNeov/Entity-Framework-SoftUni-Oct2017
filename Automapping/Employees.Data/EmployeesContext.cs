namespace Employees.Data
{
    using Microsoft.EntityFrameworkCore;
    using Employees.Models;
    public class EmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeesContext() { }

        public EmployeesContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName)
                .IsRequired(true)
                .HasMaxLength(60);

                entity.Property(e => e.LastName)
                .IsRequired(true)
                .HasMaxLength(60);

                entity.Property(e => e.Address)
                .HasMaxLength(250);
            });
        }
    }
}