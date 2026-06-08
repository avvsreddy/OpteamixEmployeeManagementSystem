using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class AssignRoleDto
    {
        public string Email { get; set; }
        public string Role { get; set; } // "Admin", "Manager", "Employee"
    }
}
