﻿namespace FormsApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }


    }
}
