using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Data
{
    internal class EmployeeDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True; Initial Catalog=OpteamixEmployeeDbComplex;");
        }
        
    }
}
