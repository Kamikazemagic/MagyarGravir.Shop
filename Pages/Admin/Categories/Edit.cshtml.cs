using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;

namespace MagyarGravir.Shop.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;

        public EditModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Category EditCategory { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat == null)
<<<<<<< HEAD
                return RedirectToPage("/Admin/Categories/CategoryList");
=======
                return RedirectToPage("/Admin/Categories/Index");
>>>>>>> 259008b110d4a5a7f5238371c8b31c6558ccf735

            EditCategory = cat;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var cat = await _db.Categories.FindAsync(EditCategory.Id);
            if (cat == null)
<<<<<<< HEAD
                return RedirectToPage("/Admin/Categories/CategoryList");
=======
                return RedirectToPage("/Admin/Categories/Index");
>>>>>>> 259008b110d4a5a7f5238371c8b31c6558ccf735

            cat.Name = EditCategory.Name;
            cat.IsCustom = EditCategory.IsCustom;

            await _db.SaveChangesAsync();
<<<<<<< HEAD
            return RedirectToPage("/Admin/Categories/CategoryList");
=======
            return RedirectToPage("/Admin/Categories/Index");
>>>>>>> 259008b110d4a5a7f5238371c8b31c6558ccf735
        }
    }
}
