public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool IsAdmin { get; set; } = false;

    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;

    // 👇 ÚJAK
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpiry { get; set; }
}