using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
