using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MagyarGravir.Shop.Services;

namespace MagyarGravir.Shop.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly EmailService _emailService;


        public ForgotPasswordModel(AppDbContext context, PasswordService passwordService, EmailService emailSender)
        {
            _context = context;
            _passwordService = passwordService;
            _emailService = emailSender;
        }

        [BindProperty]
    
        public string Email { get; set; } = string.Empty;

        public string Message { get; set; } = "";   

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);

            if (user != null)
            {
                var token = Guid.NewGuid().ToString();
                user.ResetToken = token;
                user.ResetTokenExpiry = DateTime.Now.AddMinutes(30);

                await _context.SaveChangesAsync();

                var link = Url.Page("/Account/ResetPassword", null, new { token }, Request.Scheme);

                await _emailService.SendEmailAsync(
                    Email,
                    "Jelszó visszaállítás",
                    $"Kattints ide az új jelszóhoz: <a href='{link}'>link</a>"
                );
            }

            Message = "Ha létezik ilyen email, küldtünk levelet.";
            return Page();
        }
    }
}