using System.ComponentModel.DataAnnotations;

namespace MagyarGravir.Shop.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        // Felhasználóhoz kötés
        public int? UserId { get; set; }   // null vendégnél
        public User? User { get; set; }

        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }

        public string? CustomText { get; set; }
        public string? CustomImagePath { get; set; }
    }
}