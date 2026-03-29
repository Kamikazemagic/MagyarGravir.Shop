using System.ComponentModel.DataAnnotations;

namespace MagyarGravir.Shop.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        // Identity UserId → STRING
        public string UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public List<CartItem> Items { get; set; } = new();
    }
}