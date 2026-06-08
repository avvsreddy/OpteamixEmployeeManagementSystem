using Microsoft.AspNetCore.Identity;
using System;

namespace OpteamixEmployeeManagementSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }

        // New fields
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Department { get; set; }
        public string? Gender { get; set; }
        public string? EmployeeCode { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}