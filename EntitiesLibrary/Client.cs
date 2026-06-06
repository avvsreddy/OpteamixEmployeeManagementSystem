namespace EntitiesLibrary
{
    public class Client : BaseEntity
    {
        public string ClientName { get; set; } = string.Empty;

        public string ContactPerson { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public ICollection<Project> Projects { get; set; }
            = new List<Project>();
    }

}
