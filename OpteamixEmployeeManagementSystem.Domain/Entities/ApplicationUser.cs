using Microsoft.AspNetCore.Identity;

namespace OpteamixEmployeeManagementSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public string? Department { get; set; }

        public string? Gender { get; set; }

        public string? EmployeeCode { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }

        // New fields
        public string? OtpCode { get; set; }

        public DateTime? OtpExpiry { get; set; }

        public byte[]? ProfileImage { get; set; }
    }
}