namespace PMD_CRUD.Models.VModel
{
    public class VModel
    {
        public VModel()
        {
            this.Details = new List<Details>();
        }
        public int PId { get; set; }
        public string PName { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string? PImage { get; set; }
        public IFormFile? ImageFile { get; set; }
        public DateTime PDate { get; set; }
        public bool IsAvailable { get; set; }
        public virtual List<Details>? Details { get; set; }
    }
}
