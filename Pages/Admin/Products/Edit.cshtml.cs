using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace MagyarGravir.Shop.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public EditModel(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [BindProperty]
        public Product EditProduct { get; set; } = default!;

        [BindProperty]
        public IFormFile? UploadImage { get; set; }

        public List<Category> Categories { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return RedirectToPage("/Admin/Products/Index");

            EditProduct = product;
            Categories = await _db.Categories.OrderBy(c => c.Name).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _db.Categories.ToListAsync();
                return Page();
            }

            var product = await _db.Products.FindAsync(EditProduct.Id);
            if (product == null)
                return RedirectToPage("/Admin/Products/Index");

            // adat frissítése
            product.Name = EditProduct.Name;
            product.Description = EditProduct.Description;
            product.Price = EditProduct.Price;
            product.CategoryId = EditProduct.CategoryId;
            product.StockQuantity = EditProduct.StockQuantity;
            product.IsActive = EditProduct.IsActive;

            // --- KÉP CSERE ---
            if (UploadImage != null)
            {
                // régi törlése, ha van
                if (!string.IsNullOrEmpty(product.MainImagePath))
                {
                    string oldPath = _env.WebRootPath + product.MainImagePath.Replace("/", "\\");
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                // új kép mentése
                string folder = Path.Combine(_env.WebRootPath, "uploads/products");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(UploadImage.FileName);
                string fullPath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await UploadImage.CopyToAsync(stream);
                }

                product.MainImagePath = "/uploads/products/" + fileName;
            }

            await _db.SaveChangesAsync();

            return RedirectToPage("/Admin/Products/Index");
        }
    }
}
