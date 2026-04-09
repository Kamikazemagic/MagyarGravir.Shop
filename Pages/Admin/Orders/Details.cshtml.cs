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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Order = await _db.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (Order == null)
                return RedirectToPage("/Admin/Orders/OrderList");

            return Page();
        }

        public async Task<IActionResult> OnPostCompleteAsync(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return RedirectToPage("/Admin/Orders/OrderList");

            order.Status = "Completed";

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"A rendelés #{order.Id} teljesítve lett.";
            return RedirectToPage("/Admin/Orders/OrderList");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return RedirectToPage("./Index");

            order.Status = "Deleted";

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"A rendelés #{order.Id} törölve lett.";
            return RedirectToPage("/Admin/Orders/OrderList");
        }

        public async Task<IActionResult> OnPostPendingAsync(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return RedirectToPage("./Index");

            order.Status = "Pending";

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"A rendelés #{order.Id} feldolgozás alatt van.";
            return RedirectToPage("/Admin/Orders/OrderList");
        }
        public async Task<IActionResult> OnPostHardDeleteAsync(int id)
        {
            var order = await _db.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return RedirectToPage("/Admin/Orders/OrderList");

            _db.OrderItems.RemoveRange(order.Items);
            _db.Orders.Remove(order);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"A rendelés #{order.Id} VÉGLEGESEN törölve lett!";
            return RedirectToPage("/Admin/Orders/OrderList");
        }
    }
}
