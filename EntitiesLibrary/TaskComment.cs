namespace EntitiesLibrary
{
    public class TaskComment : BaseEntity
    {
        public string CommentText { get; set; } = string.Empty;

        public int TaskItemId { get; set; }

        public TaskItem TaskItem { get; set; } = null!;

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;
    }

}
