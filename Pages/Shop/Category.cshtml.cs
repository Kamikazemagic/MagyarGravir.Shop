using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;

namespace MagyarGravir.Shop.Pages.Shop
{
    public class CategoryModel : PageModel
    {
        private readonly AppDbContext _db;

        public CategoryModel(AppDbContext db)
        {
            _db = db;
        }

        public Category? CurrentCategory { get; set; }
        public List<Product> Products { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CurrentCategory = await _db.Categories.FindAsync(id);
            if (CurrentCategory == null)
                return RedirectToPage("/Index");

            Products = await _db.Products
                .Where(p => p.CategoryId == id && p.IsActive)
                .ToListAsync();

            return Page();
        }
    }
}
