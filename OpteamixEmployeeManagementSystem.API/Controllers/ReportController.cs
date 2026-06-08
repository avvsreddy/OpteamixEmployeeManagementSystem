using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {

        private readonly IReportRepository _repository;

        public ReportController(IReportRepository repository)
        {
            _repository = repository;
        }
        //[Authorize]
        [HttpGet]
        [Route("total-employees")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetTotalEmployees()
        {
            var count = await _repository.GetTotalEmployeesAsync();
            return Ok(count);
        }
        //[Authorize]
        [HttpGet]
        [Route("total-projects")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetTotalProjects()
        {
            var count = await _repository.GetTotalProjectsAsync();
            return Ok(count);
        }
        //[Authorize]
        [HttpGet]
        [Route("total-tasks")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetTotalTasks()
        {
            var count = await _repository.GetTotalTasksAsync();
            return Ok(count);
        }
        //[Authorize]
        [HttpGet]
        [Route("completed-tasks")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetCompletedTasks()
        {
            var count = await _repository.GetCompletedTasksAsync();
            return Ok(count);
        }
        //[Authorize]
        [HttpGet]
        [Route("pending-tasks")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetPendingTasks()
        {
            var count = await _repository.GetPendingTasksAsync();
            return Ok(count);
        }
        //[Authorize]
        [HttpGet]
        [Route("project-status")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetProjectStatusSummary()
        {
            var summary = await _repository.GetProjectStatusSummaryAsync();
            return Ok(summary);
        }
    }
}
