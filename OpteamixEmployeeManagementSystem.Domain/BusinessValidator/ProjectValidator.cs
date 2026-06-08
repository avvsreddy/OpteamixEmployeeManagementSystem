using OpteamixEmployeeManagementSystem.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.BusinessValidator
{
    public class ProjectValidator : IProjectValidator
    {
        public void Validate(CreateProjectDto dto)
        {
            if (dto.StartDate > dto.EndDate)
            {
                throw new Exception("Start Date cannot be greater than End Date");
            }

            if (dto.Budget <= 0)
            {
                throw new Exception("Budget must be greater than 0");
            }
        }

        public void Validate(UpdateProjectDto dto)
        {
            if (dto.StartDate > dto.EndDate)
            {
                throw new Exception("Start Date cannot be greater than End Date");
            }

            if (dto.Budget <= 0)
            {
                throw new Exception("Budget must be greater than 0");
            }
        }
    }
}
