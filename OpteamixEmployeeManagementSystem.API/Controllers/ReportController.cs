using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        
        private readonly IReportRepository _repository;

        public ReportController(IReportRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("total-employees")]
        public async Task<IActionResult> GetTotalEmployees()
        {
            var count = await _repository.GetTotalEmployeesAsync();
            return Ok(count);
        }

        [HttpGet]
        [Route("total-projects")]
        public async Task<IActionResult> GetTotalProjects()
        {
            var count = await _repository.GetTotalProjectsAsync();
            return Ok(count);
        }
        [HttpGet]
        [Route("total-tasks")]
        public async Task<IActionResult> GetTotalTasks()
        {
            var count = await _repository.GetTotalTasksAsync();
            return Ok(count);
        }
        [HttpGet]
        [Route("completed-tasks")]
        public async Task<IActionResult> GetCompletedTasks()
        {
            var count = await _repository.GetCompletedTasksAsync();
            return Ok(count);
        }

        [HttpGet]
        [Route("pending-tasks")]
        public async Task<IActionResult> GetPendingTasks()
        {
            var count = await _repository.GetPendingTasksAsync();
            return Ok(count);
        }
        [HttpGet]
        [Route("project-status")]
        public async Task<IActionResult> GetProjectStatusSummary()
        {
            var summary = await _repository.GetProjectStatusSummaryAsync();
            return Ok(summary);
        }
    }
}
