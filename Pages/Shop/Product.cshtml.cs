using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;
using MagyarGravir.Shop.Helpers;

namespace MagyarGravir.Shop.Pages.Shop
{
    public class ProductModel : PageModel
    {
        private readonly AppDbContext _db;

        public ProductModel(AppDbContext db)
        {
            _db = db;
        }

        public Product? CurrentProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CurrentProduct = await _db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (CurrentProduct == null)
                return RedirectToPage("/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return RedirectToPage("/Index");

            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                       ?? new List<CartItem>();

            var item = cart.FirstOrDefault(c => c.ProductId == id);

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    Image = product.MainImagePath
                });
            }
            else
            {
                item.Quantity++;
            }

            HttpContext.Session.SetObject("cart", cart);

            return RedirectToPage("/Shop/Cart");
        }
    }
}
