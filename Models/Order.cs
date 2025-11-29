using System;
using System.Collections.Generic;

namespace MagyarGravir.Shop.Models
{
    public class Order
    {
        public int Id { get; set; }

        // vásárló adatai
        public string CustomerName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // admin állapot
        public bool IsCompleted { get; set; } = false;

        // rendelés sorok
        public List<OrderItem> Items { get; set; } = new();
    }
}
