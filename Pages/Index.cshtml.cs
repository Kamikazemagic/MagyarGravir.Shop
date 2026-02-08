using Microsoft.AspNetCore.Mvc.RazorPages;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;

namespace MagyarGravir.Shop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public List<Category> Categories { get; set; } = new();
        public List<Product> FeaturedProducts { get; set; } = new();

        public void OnGet()



        {
            Categories = _db.Categories
                .OrderBy(c => c.Id)
                .ToList();

            FeaturedProducts = _db.Products
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.Id)
                .Take(6)
                .ToList();
        }
    }
}
