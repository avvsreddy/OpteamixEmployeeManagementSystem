using OpteamixEmployeeManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public static class ProjectConversions
    {
        // Entity → DTO
        public static ProjectDto FromEntity(Project project)
        {
            return new ProjectDto
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ClientName = project.ClientName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Budget = project.Budget,
                Status = project.Status,
                ManagerId = project.ManagerId
            };
        }

        // CreateProjectDto → Entity
        public static Project ToEntity(CreateProjectDto dto)
        {
            return new Project
            {
                ProjectName = dto.ProjectName,
                ClientName = dto.ClientName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Budget = dto.Budget,
                Status = dto.Status,
                ManagerId = dto.ManagerId
            };
        }

        // UpdateProjectDto → Entity
        public static Project ToEntity(UpdateProjectDto dto)
        {
            return new Project
            {
                ProjectName = dto.ProjectName,
                ClientName = dto.ClientName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Budget = dto.Budget,
                Status = dto.Status,
                ManagerId = dto.ManagerId
            };
        }
    }
}
