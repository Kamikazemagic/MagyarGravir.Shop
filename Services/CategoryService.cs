using System.Collections.Generic;
using System.Linq;
using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;

namespace MagyarGravir.Shop.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
    }
}