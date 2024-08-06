namespace theliner_api.Models
{
    public class User : IAuditableEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
