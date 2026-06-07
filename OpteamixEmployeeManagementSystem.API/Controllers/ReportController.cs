using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.API.Controllers
{
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

        [HttpGet("total-projects")]
        public async Task<IActionResult> GetTotalProjects()
        {
            var count = await _repository.GetTotalProjectsAsync();
            return Ok(count);
        }
        [HttpGet("project-status")]
        public async Task<IActionResult> GetProjectStatusSummary()
        {
            var summary = await _repository.GetProjectStatusSummaryAsync();
            return Ok(summary);
        }
    }
}
