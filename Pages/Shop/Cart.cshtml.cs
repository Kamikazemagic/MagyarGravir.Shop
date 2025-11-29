using Microsoft.AspNetCore.Mvc.RazorPages;
using MagyarGravir.Shop.Models;
using MagyarGravir.Shop.Helpers;

namespace MagyarGravir.Shop.Pages.Shop
{
    public class CartModel : PageModel
    {
        public List<CartItem> Cart { get; set; } = new();

        public void OnGet()
        {
            Cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                   ?? new List<CartItem>();
        }
    }
}
