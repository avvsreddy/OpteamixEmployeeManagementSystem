using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;
using OpteamixEmployeeManagementSystem.Domain.DTOs;

namespace OpteamixEmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController(
            IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees =
                await _repository.GetEmployeesAsync();

            return Ok(
                employees.Select(
                    EmployeeConversions.FromEntity));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(
            int id)
        {
            var employee =
                await _repository.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(EmployeeConversions.FromEntity(employee));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployees(
            string keyword)
        {
            var employees =
                await _repository.SearchEmployeesAsync(keyword);

            return Ok(
    employees.Select(
        EmployeeConversions.FromEntity));
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(
    [FromBody] CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee =
                EmployeeConversions.ToEntity(dto);

            var result =
                await _repository.AddEmployeeAsync(employee);

            return CreatedAtAction(
                nameof(GetEmployeeById),
                new { id = result.EmployeeId },
                EmployeeConversions.FromEntity(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(
    int id,
    [FromBody] UpdateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.EmployeeId)
            {
                return BadRequest(
                    "Employee ID mismatch");
            }

            var employee =
                EmployeeConversions.ToEntity(dto);

            var result =
                await _repository.UpdateEmployeeAsync(employee);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(
                EmployeeConversions.FromEntity(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(
            int id)
        {
            var result =
                await _repository.DeleteEmployeeAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}