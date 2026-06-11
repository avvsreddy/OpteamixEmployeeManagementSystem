
using System.ComponentModel.DataAnnotations;

namespace OpteamixEmployeeManagementSystem.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Code is required")]
        [StringLength(20)]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [StringLength(
            100,
            MinimumLength = 3,
            ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(
            ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(
            @"^\d{10}$",
            ErrorMessage = "Phone Number must contain exactly 10 digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Designation is required")]
        [StringLength(100)]
        public string Designation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Joining Date is required")]
        public DateTime JoiningDate { get; set; }

        [Range(
            0,
            double.MaxValue,
            ErrorMessage = "Salary must be positive")]
        public decimal Salary { get; set; }

        // Soft Delete
        public bool IsDeleted { get; set; } = false;

        // ==========================
        // Department Relationship
        // ==========================

        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        

       
    }
}