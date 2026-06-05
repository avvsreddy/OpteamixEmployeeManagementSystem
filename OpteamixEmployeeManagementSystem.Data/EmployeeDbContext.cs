using Microsoft.EntityFrameworkCore;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}