using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MagyarGravir.Shop.Models;
using MagyarGravir.Shop.Helpers;
using MagyarGravir.Shop.Data;

namespace MagyarGravir.Shop.Pages.Shop
{
    public class CheckoutModel : PageModel
    {
        private readonly AppDbContext _db;

        public CheckoutModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public string CustomerName { get; set; } = "";

        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Address { get; set; } = "";

        [BindProperty]
        public string? Note { get; set; }

        public List<CartItem> Cart { get; set; } = new();

        public void OnGet()
        {
            Cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                   ?? new List<CartItem>();
        }

        public IActionResult OnPost()
        {
            Cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                   ?? new List<CartItem>();

            if (Cart.Count == 0)
                return RedirectToPage("/Shop/Cart");

            // rendelés létrehozása
            var order = new Order
            {
                CustomerName = CustomerName,
                Email = Email,
                Address = Address,
                Note = Note,
                Items = Cart.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    ProductName = c.Name,
                    UnitPrice = c.Price,
                    Quantity = c.Quantity
                }).ToList()
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            // kosár ürítése
            HttpContext.Session.Remove("cart");

            // redirect a köszönõoldalra
            return RedirectToPage("/Shop/Success", new { id = order.Id });
        }
    }
}
