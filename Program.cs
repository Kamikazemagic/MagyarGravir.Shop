using Microsoft.EntityFrameworkCore;
using MagyarGravir.Shop.Data;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Razor Pages
builder.Services.AddRazorPages();

// SessionExtensions
builder.Services.AddSession();

//Shop Pages
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Shop/Index", "/Shop");
});


// Cookie-s admin auth
builder.Services.AddAuthentication("AdminCookie")
    .AddCookie("AdminCookie", options =>
    {
        options.LoginPath = "/Admin/Login";
        options.AccessDeniedPath = "/Admin/Login";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// ?? Hibakódok megjelenítése fejlesztés közben
app.UseStatusCodePages();

app.MapRazorPages();

app.Run();
