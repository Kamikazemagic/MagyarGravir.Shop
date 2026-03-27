using MagyarGravir.Shop.Data;
using MagyarGravir.Shop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MagyarGravir.Shop.Pages.Account;

public class ResetPasswordModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwordService;

    public ResetPasswordModel(AppDbContext context, PasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    [BindProperty]
    public string Token { get; set; } = string.Empty;

    [BindProperty]
    public string NewPassword { get; set; } = string.Empty;

    public string? Message { get; set; }

    public void OnGet(string token)
    {
        Token = token;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.ResetToken == Token);

        if (user == null || user.ResetTokenExpiry < DateTime.Now)
        {
            Message = "Érvénytelen vagy lejárt link";
            return Page();
        }

        user.Password = _passwordService.HashPassword(NewPassword);
        user.ResetToken = null;
        user.ResetTokenExpiry = null;

        await _context.SaveChangesAsync();

        Message = "Jelszó sikeresen frissítve!";
        return Page();
    }
}