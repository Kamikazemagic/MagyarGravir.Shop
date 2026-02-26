using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MagyarGravir.Shop.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _db;

        public LoginModel(AppDbContext db) => _db = db;

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == Username);
            if (user == null)
            {
                TempData["Error"] = "Hibás felhasználónév vagy jelszó!";
                return Page();
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, Password);

            if (result == PasswordVerificationResult.Success)
            {
                // Session-be mentés
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin ? "true" : "false");

                TempData["Message"] = $"Üdv, {user.Username}!";
                return RedirectToPage("/Index");
            }

            TempData["Error"] = "Hibás felhasználónév vagy jelszó!";
            return Page();
        }
    }
}       