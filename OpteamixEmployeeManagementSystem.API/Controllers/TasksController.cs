using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TasksController(
            ITaskRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasks()
        {
            var tasks =
                await _repository.GetTasksAsync();

            return Ok(
                _mapper.Map<List<TaskDto>>(tasks));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task =
                await _repository.GetTaskByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(
                _mapper.Map<TaskDto>(task));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTask(CreateTaskDto dto)
        {
            var task =
                _mapper.Map<TaskItem>(dto);

            var result =
                await _repository.AddTaskAsync(task);

            if (result == null)
            {
                return BadRequest(
                    "Invalid EmployeeId or ProjectId");
            }

            return Ok(
                _mapper.Map<TaskDto>(result));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTask(
            int id,
            UpdateTaskDto dto)
        {
            if (id != dto.TaskId)
            {
                return BadRequest(
                    "Task ID mismatch");
            }

            var task =
                _mapper.Map<TaskItem>(dto);

            var result =
                await _repository.UpdateTaskAsync(task);

            return Ok(
                _mapper.Map<TaskDto>(result));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result =
                await _repository.DeleteTaskAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(
                "Task deleted successfully");
        }

        [HttpGet("employee/{employeeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasksByEmployeeId(
            int employeeId)
        {
            var tasks =
                await _repository
                    .GetTasksByEmployeeIdAsync(
                        employeeId);

            return Ok(
                _mapper.Map<List<TaskDto>>(tasks));
        }

        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasksByStatus(
            string status)
        {
            var tasks =
                await _repository
                    .GetTasksByStatusAsync(
                        status);

            return Ok(
                _mapper.Map<List<TaskDto>>(tasks));
        }

        [HttpGet("priority/{priority}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasksByPriority(
            string priority)
        {
            var tasks =
                await _repository
                    .GetTasksByPriorityAsync(
                        priority);

            return Ok(
                _mapper.Map<List<TaskDto>>(tasks));
        }

        [HttpPatch("{taskId:int}/assign/{employeeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignTask(
            int taskId,
            int employeeId)
        {
            var result =
                await _repository
                    .AssignTaskAsync(
                        taskId,
                        employeeId);

            if (!result)
            {
                return BadRequest(
                    "Employee not found or Task not found");
            }

            return Ok(
                "Task assigned successfully");
        }

        [HttpPatch("{taskId:int}/status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTaskStatus(
            int taskId,
            string status)
        {
            var result =
                await _repository
                    .UpdateTaskStatusAsync(
                        taskId,
                        status);

            if (!result)
            {
                return BadRequest(
                    "Invalid task or status");
            }

            return Ok(
                "Task status updated");
        }

        [HttpPatch("{taskId:int}/priority/{priority}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTaskPriority(
            int taskId,
            string priority)
        {
            var result =
                await _repository
                    .UpdateTaskPriorityAsync(
                        taskId,
                        priority);

            if (!result)
            {
                return BadRequest(
                    "Invalid task or priority");
            }

            return Ok(
                "Task priority updated");
        }
    }
}