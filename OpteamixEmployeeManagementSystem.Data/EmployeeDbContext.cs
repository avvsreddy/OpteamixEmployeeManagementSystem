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

        // =========================
        // DbSets
        // =========================

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // Employee ↔ Department
            // =========================

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Project ↔ Employee (Manager)
            // =========================

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Manager)
                .WithMany()
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Project Configuration
            // =========================

            modelBuilder.Entity<Project>()
                .Property(p => p.Budget)
                .HasColumnType("decimal(18,2)");

            // =========================
            // Task Configuration
            // =========================

            //modelBuilder.Entity<TaskItem>()
            //    .HasKey(t => t.TaskItemId);

            //modelBuilder.Entity<TaskItem>()
            //    .HasOne(t => t.Employee)
            //    //.WithMany(e => e.AssignedTasks)
            //    .HasForeignKey(t => t.EmployeeId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TaskItem>()
            //    .HasOne(t => t.Project)
            //    .WithMany()
            //    .HasForeignKey(t => t.ProjectId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}