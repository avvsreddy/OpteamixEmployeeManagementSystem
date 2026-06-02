using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController
        (
            IEmployeeRepository repository
        )
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_repository.GetEmployees());
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee =
                _repository.GetEmployeeById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            return Ok
            (
                _repository.AddEmployee(employee)
            );
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            return Ok
            (
                _repository.UpdateEmployee(employee)
            );
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            return Ok
            (
                _repository.DeleteEmployee(id)
            );
        }
    }
}