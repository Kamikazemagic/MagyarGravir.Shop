using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;

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
    }
}