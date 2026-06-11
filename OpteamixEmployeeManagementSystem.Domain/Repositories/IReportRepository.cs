using OpteamixEmployeeManagementSystem.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.Repositories
{
    public interface IReportRepository
    {
        Task<int> GetTotalEmployeesAsync();

        Task<int> GetTotalProjectsAsync();

        Task<int> GetTotalTasksAsync();

        Task<int> GetCompletedTasksAsync();

        Task<int> GetPendingTasksAsync();

        Task<ProjectStatusSummaryDto> GetProjectStatusSummaryAsync();
    }
}