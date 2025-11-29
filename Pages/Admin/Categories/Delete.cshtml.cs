using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;

namespace MagyarGravir.Shop.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _db;

        public DeleteModel(AppDbContext db)
        {
            _db = db;
        }

        public Category? CategoryToDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CategoryToDelete = await _db.Categories.FindAsync(id);
            if (CategoryToDelete == null)
                return RedirectToPage("/Admin/Categories/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat != null)
            {
                _db.Categories.Remove(cat);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/Admin/Categories/Index");
        }
    }
}
