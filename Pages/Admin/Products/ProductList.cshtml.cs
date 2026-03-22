using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;

namespace MagyarGravir.Shop.Pages.Admin.Products
{
    public class ProductListModel : PageModel
    {
        private readonly AppDbContext _db;

        public ProductListModel(AppDbContext db)
        {
            _db = db;
        }

        public List<Product> ProductList { get; set; } = new();

        public async Task OnGetAsync()
        {
            ProductList = await _db.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}
