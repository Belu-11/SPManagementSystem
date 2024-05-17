using System.ComponentModel.DataAnnotations;

namespace CoreBusiness
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        // Navigation property for EF Core
        public List<Product>? Products { get; set; }
    }
}
