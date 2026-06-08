using OpteamixEmployeeManagementSystem.Domain.Entities;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public static class TaskConversions
    {
        public static TaskDto FromEntity(TaskItem task)
        {
            return new TaskDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                EmployeeId = task.EmployeeId,
                ProjectId = task.ProjectId
            };
        }

        public static TaskItem ToEntity(CreateTaskDto dto)
        {
            return new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                EmployeeId = dto.EmployeeId,
                ProjectId = dto.ProjectId
            };
        }

        public static TaskItem ToEntity(UpdateTaskDto dto)
        {
            return new TaskItem
            {
                TaskId = dto.TaskId,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                EmployeeId = dto.EmployeeId,
                ProjectId = dto.ProjectId
            };
        }
    }
}