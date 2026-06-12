using System;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public DateTime JoiningDate { get; set; }

        public decimal Salary { get; set; }
    }
}