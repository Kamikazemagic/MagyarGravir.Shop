using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Helpers;
using MagyarGravir.Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MagyarGravir.Shop.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _db;

        public RegisterModel(AppDbContext db) => _db = db;

        [BindProperty] public string Username { get; set; } = string.Empty;
        [BindProperty] public string Email { get; set; } = string.Empty;
        [BindProperty] public string ZipCode { get; set; } = string.Empty;
        [BindProperty] public string City { get; set; } = string.Empty;
        [BindProperty] public string Street { get; set; } = string.Empty;
        [BindProperty] public string Password { get; set; } = string.Empty;
        [BindProperty] public string ConfirmPassword { get; set; } = string.Empty;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (Password != ConfirmPassword)
            {
                TempData["Error"] = "A jelszavak nem egyeznek!";
                return Page();
            }

            if (_db.Users.Any(u => u.Username == Username))
            {
                TempData["Error"] = "Ez a felhasználónév már foglalt!";
                return Page();
            }

            if (_db.Users.Any(u => u.Email == Email))
            {
                TempData["Error"] = "Ez az email cím már regisztrálva van!";
                return Page();
            }

            var user = new User
            {
                Username = Username,
                Email = Email,
                ZipCode = ZipCode,
                City = City,
                Street = Street,
                IsAdmin = false
            };

            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, Password);

            _db.Users.Add(user);
            _db.SaveChanges(); // user.Id létrejön

            // === Session kosár mentése a felhasználóhoz ===
            var sessionCart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();

            foreach (var item in sessionCart)
            {
                _db.CartItems.Add(new CartItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UserId = user.Id
                });
            }

            _db.SaveChanges();

            // Session törlése, mert a kosár már az adatbázisban van
            HttpContext.Session.Remove("cart");

            TempData["Message"] = "Sikeres regisztráció! Most bejelentkezhet.";
            return RedirectToPage("/Account/Login");
        }
    }
}