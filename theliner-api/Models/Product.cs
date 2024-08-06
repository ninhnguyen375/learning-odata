using System.ComponentModel.DataAnnotations;

namespace theliner_api.Models
{
    public class Product : IAuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
