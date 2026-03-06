using Microsoft.AspNetCore.Mvc.RazorPages;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;

namespace MagyarGravir.Shop.Pages.Shop
{
    public class SuccessModel : PageModel
    {
        private readonly AppDbContext _db;

        public SuccessModel(AppDbContext db)
        {
            _db = db;
        }

        public Order? Order { get; set; }

        public void OnGet(int id)
        {
            Order = _db.Orders.FirstOrDefault(o => o.Id == id);
        }
    }
}
