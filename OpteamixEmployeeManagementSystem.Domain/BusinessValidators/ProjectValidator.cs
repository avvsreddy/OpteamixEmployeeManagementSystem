using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.BusinessValidators
{
    public class ProjectValidator : IProjectValidator
    {
        public void Validate(CreateProjectDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ProjectName))
            {
                throw new Exception("Project Name is required");
            }

            if (string.IsNullOrWhiteSpace(dto.ClientName))
            {
                throw new Exception("Client Name is required");
            }

            if (dto.StartDate > dto.EndDate)
            {
                throw new Exception("Start Date cannot be greater than End Date");
            }

            if (dto.Budget <= 0)
            {
                throw new Exception("Budget must be greater than 0");
            }
            if (dto.ManagerId == 0)
            {
                throw new Exception("Please select a valid Manager Id");
            }
        }

        public void Validate(UpdateProjectDto dto)
        {
            if (dto.ProjectId <= 0)
            {
                throw new Exception("Invalid ProjectId");
            }

            if (string.IsNullOrWhiteSpace(dto.ProjectName))
            {
                throw new Exception("Project Name is required");
            }

            if (string.IsNullOrWhiteSpace(dto.ClientName))
            {
                throw new Exception("Client Name is required");
            }

            if (dto.StartDate > dto.EndDate)
            {
                throw new Exception("Start Date cannot be greater than End Date");
            }

            if (dto.Budget <= 0)
            {
                throw new Exception("Budget must be greater than 0");
            }

            if (dto.ManagerId == 0)
            {
                throw new Exception("Please select a valid Manager Id");
            }
        }
    }
}
