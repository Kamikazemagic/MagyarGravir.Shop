using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;

namespace MagyarGravir.Shop.Pages.Admin.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _db;

        public DetailsModel(AppDbContext db)
        {
            _db = db;
        }

        public Order? Order { get; set; }

        // Lekérjük a rendelést a részletekhez
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Order = await _db.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (Order == null)
                return RedirectToPage("./Index");

            return Page();
        }

        // Teljesítés = fizikai törlés
        public async Task<IActionResult> OnPostCompleteAsync(int id)
        {
            var order = await _db.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return RedirectToPage("./Index");

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            // TempData-ban üzenet
            TempData["SuccessMessage"] = $"A rendelés #{order.Id} teljesítve és törölve lett.";

            // Vissza a listaoldalra
            return RedirectToPage("./Index");
        }
    }
}