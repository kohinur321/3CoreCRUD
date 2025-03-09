using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMD_CRUD.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PId { get; set; }
        public string PName { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? PImage { get; set; }
        public int Quantity { get; set; }
        public DateTime PDate { get; set; }
        public bool IsAvailable { get; set; }
        public virtual IList<Details>? Details { get; set; }
    }
}
