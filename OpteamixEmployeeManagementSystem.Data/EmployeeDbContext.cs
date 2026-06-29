using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpteamixEmployeeManagementSystem.Domain.Entities;

namespace OpteamixEmployeeManagementSystem.Data
{
    public class EmployeeDbContext : IdentityDbContext<ApplicationUser>
    {
        public EmployeeDbContext(
            DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =====================================
            // Employee -> Department
            // =====================================

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================================
            // Project -> Manager(Employee)
            // =====================================

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Manager)
                .WithMany()
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================================
            // Task -> Employee
            // =====================================

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Employee)
                .WithMany()
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================================
            // Task -> Project
            // =====================================

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Project)
                .WithMany()
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // =====================================
            // Decimal Precision
            // =====================================

            modelBuilder.Entity<Project>()
                .Property(p => p.Budget)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18,2)");

            // =====================================
            // Global Query Filters (Soft Delete)
            // =====================================

            modelBuilder.Entity<Employee>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Project>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Department>()
                .HasQueryFilter(d => !d.IsDeleted);

            // Hide tasks whose project is soft deleted

            modelBuilder.Entity<TaskItem>()
                .HasQueryFilter(t =>
                    t.ProjectId == null ||
                    !t.Project!.IsDeleted);
        }
    }
}