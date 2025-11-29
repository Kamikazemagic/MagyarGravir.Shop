using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MagyarGravir.Shop.Pages.Admin
{
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
