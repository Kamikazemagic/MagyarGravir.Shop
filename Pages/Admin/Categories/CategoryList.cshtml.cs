using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;

namespace MagyarGravir.Shop.Pages.Admin.Categories
{
    public class CategoryListModel : PageModel
    {
        private readonly AppDbContext _db;

        public CategoryListModel(AppDbContext db)
        {
            _db = db;
        }

        public List<Category> Categories { get; set; } = new();

        public void OnGet()
        {
            Categories = _db.Categories
                .OrderBy(c => c.Id)
                .ToList();
        }
    }
}
