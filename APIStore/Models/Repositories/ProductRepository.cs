using System.Xml.Linq;

namespace APIStore.Models.Repositories
{
    public class ProductRepository
    {
        private static List<Product> products = new List<Product>() {
            new Product{ ProdId = 1,Name= "Name1", Brand = "My Brand", Color = "Blue", Gender = "Men", Price=30, Size=10},
            new Product{ ProdId = 2,Name= "Name2", Brand = "My Brand", Color = "Black", Gender = "Men", Price=35, Size=12},
            new Product{ ProdId = 3,Name= "Name3", Brand = "Your Brand", Color = "Pink", Gender = "Women", Price=28, Size=8},
            new Product{ ProdId = 4,Name= "Name4", Brand = "Your Brand", Color = "Yellow", Gender = "Women", Price=30, Size=9}
        };
        public static List<Product> GetProducts()
        {
            return products;
        }
        public static bool ProductExists(int i)
        {
            return products.Any(x => x.ProdId == i);
        }
        public static Product? GetProductByID(int id)
        {
            return products.FirstOrDefault(x => x.ProdId == id);
        }
        public static Product GetProductByProperties(string? name, string? brand, string? gender, string? color, int? size)
        {
            return products.FirstOrDefault(x =>
            !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(x.Name) &&
            x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrWhiteSpace(brand) && !string.IsNullOrWhiteSpace(x.Brand) &&
            x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrWhiteSpace(gender) &&
            !string.IsNullOrWhiteSpace(x.Gender) &&
            x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrWhiteSpace(color) &&
            !string.IsNullOrWhiteSpace(x.Color) &&
            x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
            size.HasValue && x.Size.HasValue && size.Value == x.Size.Value);
        }
        public static void AddProduct(Product Product)
        {
            int maxId = products.Max(x => x.ProdId);
            Product.ProdId = maxId + 1;
            products.Add(Product);
        }
        public static void UpdateProduct(Product Product)
        {
            Product ProductToUpdate = products.First(x => x.ProdId == Product.ProdId);
            ProductToUpdate.Name = Product.Name;
            ProductToUpdate.Brand = Product.Brand;
            ProductToUpdate.Price = Product.Price;
            ProductToUpdate.Gender = Product.Gender;
            ProductToUpdate.Color = Product.Color;
            ProductToUpdate.Size = Product.Size;
        }
        public static void DeleteProduct(int Id)
        {
            var Product = GetProductByID(Id);
            if (Product != null)
            {
                products.Remove(Product);
            }
        }
    }
}
