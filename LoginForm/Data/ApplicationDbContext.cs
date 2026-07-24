using LoginForm.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginForm.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EducationDetail> educationDetails { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<EducationDetail>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.EducationDetails)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.department)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.Department_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
