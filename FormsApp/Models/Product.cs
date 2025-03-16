using System.ComponentModel;

namespace FormsApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }


    }
}
