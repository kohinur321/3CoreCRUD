using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMD_CRUD.Models
{
    public class Details
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DId { get; set; }
        [ForeignKey("Product")]
        public int PId { get; set; }
        [ForeignKey("Category")]
        public int CId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Category? Category { get; set; }
    }
}
