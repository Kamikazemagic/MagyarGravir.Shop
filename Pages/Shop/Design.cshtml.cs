using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Helpers;
using MagyarGravir.Shop.Models;

namespace MagyarGravir.Shop.Pages.Shop
{
    public class DesignModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public DesignModel(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [BindProperty(SupportsGet = true)]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [BindProperty]
        public string? CustomText { get; set; }

        // "text" vagy "image"
        [BindProperty]
        public string DesignMode { get; set; } = "text";

        [BindProperty]
        public IFormFile? UploadImage { get; set; }

        public string InitialText => "Itt jelenik meg a feliratod";

        public async Task<IActionResult> OnGetAsync()
        {
            if (ProductId == 0)
                return RedirectToPage("/Index");

            Product = await _db.Products.FirstOrDefaultAsync(p => p.Id == ProductId && p.IsActive);
            if (Product == null)
                return RedirectToPage("/Index");

            if (string.IsNullOrWhiteSpace(CustomText))
            {
                CustomText = InitialText;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Product = await _db.Products.FirstOrDefaultAsync(p => p.Id == ProductId && p.IsActive);
            if (Product == null)
                return RedirectToPage("/Index");

            if (!ModelState.IsValid)
                return Page();

            string? savedImagePath = null;

            if (UploadImage != null && UploadImage.Length > 0)
            {
                var uploadsRoot = Path.Combine(_env.WebRootPath, "uploads", "custom");
                Directory.CreateDirectory(uploadsRoot);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(UploadImage.FileName)}";
                var filePath = Path.Combine(uploadsRoot, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await UploadImage.CopyToAsync(stream);
                }

                savedImagePath = "/uploads/custom/" + fileName;
            }

            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();

            cart.Add(new CartItem
            {
                ProductId = Product.Id,
                Name = Product.Name + " (egyedi terv)",
                Price = Product.Price,
                Quantity = 1,
                Image = Product.MainImagePath,
                CustomText = DesignMode == "text" ? CustomText : null,
                CustomImagePath = DesignMode == "image" ? savedImagePath : null
            });

            HttpContext.Session.SetObject("cart", cart);

            return RedirectToPage("/Shop/Cart");
        }
    }
}
