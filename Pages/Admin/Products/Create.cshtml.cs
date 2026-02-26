using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace MagyarGravir.Shop.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public CreateModel(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [BindProperty]
        public Product NewProduct { get; set; } = new Product();

        [BindProperty]
        public IFormFile? UploadImage { get; set; }

        public List<Category> Categories { get; set; } = new();

        public async Task OnGetAsync()
        {
            Categories = await _db.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _db.Categories.ToListAsync();
                return Page();
            }

            // --- KÉP MENTÉSE ---
            if (UploadImage != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "uploads/products");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(UploadImage.FileName);
                string fullPath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await UploadImage.CopyToAsync(stream);
                }

                // kép url: /uploads/products/xyz.jpg
                NewProduct.MainImagePath = "/uploads/products/" + fileName;
            }

            _db.Products.Add(NewProduct);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
