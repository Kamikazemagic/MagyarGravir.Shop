using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MagyarGravir.Shop.Pages.Admin.Orders
{
    public class OrderListModel : PageModel
    {
        private readonly AppDbContext _db;

        public OrderListModel(AppDbContext db)
        {
            _db = db;
        }

        public List<Order> Orders { get; set; } = new();

        public async Task OnGetAsync()
        {
            Orders = await _db.Orders
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostResetTestDataAsync()
        {
            var items = await _db.OrderItems.ToListAsync();
            var orders = await _db.Orders.ToListAsync();

            _db.OrderItems.RemoveRange(items);
            _db.Orders.RemoveRange(orders);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Teszt adatok törölve.";

            return RedirectToPage();
        }
    }
}