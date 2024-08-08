using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace theliner_api.Models
{
    public class ProductDetail : IAuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product.Id")]
        public int ProductID { get; set; }
        public string Desc { get; set; } = "";
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
