namespace EntitiesLibrary
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;
    }

}
