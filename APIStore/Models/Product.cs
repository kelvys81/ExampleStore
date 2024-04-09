using APIStore.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace APIStore.Models
{
    public class Product
    {
        public int ProdId { get; set; }

        [Required] 
        public string? Name { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Color { get; set; }

        [Product_EnsureCorrectSizing_]
        public int? Size { get; set; }

        [Required]
        public string? Gender { get; set; }
        public double? Price { get; set; }
    }
}
