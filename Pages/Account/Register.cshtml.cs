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

        public void OnGet()
        {
            // Oldal betöltésekor minden mező üres legyen
            Username = string.Empty;
            Email = string.Empty;
            ZipCode = string.Empty;
            City = string.Empty;
            Street = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }

        public IActionResult OnPost()
        {
            // Jelszó egyezés ellenőrzése
            if (Password != ConfirmPassword)
            {
                TempData["Error"] = "A jelszavak nem egyeznek!";
                ClearFields();
                return Page();
            }

            // Felhasználónév foglalt vagy tiltott ellenőrzése
            if (_db.Users.Any(u => u.Username.ToLower() == Username.ToLower()) || Username.ToLower() == "admin")
            {
                TempData["Error"] = "Ez a felhasználónév nem használható!";
                ClearFields();
                return Page();
            }

            // Email foglalt ellenőrzése
            if (_db.Users.Any(u => u.Email.ToLower() == Email.ToLower()))
            {
                TempData["Error"] = "Ez az email cím már regisztrálva van!";
                ClearFields();
                return Page();
            }

            // Új felhasználó létrehozása
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

            // Session kosár mentése
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
            HttpContext.Session.Remove("cart");

            TempData["Message"] = "Sikeres regisztráció! Most bejelentkezhet.";
            return RedirectToPage("/Account/Login");
        }

        private void ClearFields()
        {
            // Hibánál minden mező törlése
            Username = string.Empty;
            Email = string.Empty;
            ZipCode = string.Empty;
            City = string.Empty;
            Street = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
    }
}