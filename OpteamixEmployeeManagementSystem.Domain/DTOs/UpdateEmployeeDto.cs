using System;
using System.ComponentModel.DataAnnotations;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class UpdateEmployeeDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(
            @"^\d{10}$",
            ErrorMessage = "Phone number must contain exactly 10 digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string Designation { get; set; } = string.Empty;

        [Required]
        public DateTime JoiningDate { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}