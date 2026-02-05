using Microsoft.AspNetCore.Mvc.RazorPages;
using MagyarGravir.Shop.Models;
using MagyarGravir.Shop.Helpers;
using Microsoft.AspNetCore.Mvc;

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

        // (+) gomb
    public IActionResult OnPostIncrease(int productId)
    {
        Cart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();
        var item = Cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
            item.Quantity++;

        HttpContext.Session.SetObject("cart", Cart); // session frissítése
        return RedirectToPage();
    }

        // (-) gomb
    public IActionResult OnPostDecrease(int productId)
    {
        Cart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();
        var item = Cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            item.Quantity--;
            if (item.Quantity <= 0)
                Cart.Remove(item);
        }

        HttpContext.Session.SetObject("cart", Cart); // session frissítése
        return RedirectToPage();
    }
    }
}
