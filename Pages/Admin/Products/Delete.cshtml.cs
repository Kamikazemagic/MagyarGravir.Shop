using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace MagyarGravir.Shop.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public DeleteModel(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public Product? ProductToDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ProductToDelete = await _db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (ProductToDelete == null)
                return RedirectToPage("/Admin/Products/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return RedirectToPage("/Admin/Products/Index");

            // Kép törlése, ha létezik
            if (!string.IsNullOrEmpty(product.MainImagePath))
            {
                string imagePath = _env.WebRootPath + product.MainImagePath.Replace("/", "\\");
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
