using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string? Name { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal? Price { get; set; }

        [Required]
        public string? Image { get; set; } 

        public bool IsAvailable { get; set; }

        [Required]
        [DisplayName("Category")]
        public int? CategoryId { get; set; }


    }
}
