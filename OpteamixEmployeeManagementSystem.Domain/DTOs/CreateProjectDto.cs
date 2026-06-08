using OpteamixEmployeeManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class CreateProjectDto
    {
        [Required(ErrorMessage = "Project name is required")]
        [MaxLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Client name is required")]
        [MaxLength(100)]
        public string ClientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public ProjectStatus Status { get; set; }

        public int? ManagerId { get; set; }
    }
}
