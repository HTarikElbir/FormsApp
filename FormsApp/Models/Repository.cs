namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();

        private static readonly List<Category> _categories = new();


        static Repository()
        {
            _categories.Add(new Category { CategoryId = 1, Name = "PC" });
            _categories.Add(new Category { CategoryId = 2, Name = "Phone" });
            _products.Add(new Product { ProductId = 1, Name = "Iphone 13", Price = 35000, Image = "1.jpg", IsAvailable = true, CategoryId = 2 });
            _products.Add(new Product { ProductId = 2, Name = "Iphone 14", Price = 45000, Image = "2.jpg", IsAvailable = true, CategoryId = 2 });
            _products.Add(new Product { ProductId = 3, Name = "Iphone 15", Price = 55000, Image = "3.jpg", IsAvailable = true, CategoryId = 2 });
            _products.Add(new Product { ProductId = 4, Name = "Iphone 16", Price = 65000, Image = "4.jpg", IsAvailable = true, CategoryId = 2 });
            _products.Add(new Product { ProductId = 5, Name = "MacBook Air", Price = 75000, Image = "5.jpg", IsAvailable = true, CategoryId = 1 });
            _products.Add(new Product { ProductId = 6, Name = "MacBook Pro", Price = 85000, Image = "6.jpg", IsAvailable = true, CategoryId = 1 });
        }

        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }

        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }

        // Create Product
        public static void CreateProduct(Product product)
        {
            product.ProductId = _products.Max(p => p.ProductId) + 1;
            _products.Add(product);
        }

        // Update Product
        public static void UpdateProduct(Product product)
        {
            var selectedProduct = _products.FirstOrDefault(p => p.ProductId == product.ProductId);

            if (selectedProduct != null) 
            {
                selectedProduct.Name = product.Name;
                selectedProduct.Price = product.Price;
                selectedProduct.Image = product.Image;
                selectedProduct.CategoryId = product.CategoryId;
                selectedProduct.IsAvailable = product.IsAvailable;
            }
            
        }
    }
}
