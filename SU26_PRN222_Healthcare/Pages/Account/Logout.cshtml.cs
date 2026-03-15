using CareDataAccess;
using CareRepositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace SU26_PRN222_Healthcare.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly ISessionRepository _sessionRepo;

        public LogoutModel(ISessionRepository sessionRepo)
        {
            _sessionRepo = sessionRepo;
        }

        public async Task OnGetAsync()
        {
            var sessionId = Request.Cookies["SessionId"];
            if (!string.IsNullOrEmpty(sessionId))
            {
                await _sessionRepo.DeleteAsync(sessionId);
                Response.Cookies.Delete("SessionId");
            }

            RedirectToPage("/Index");
        }
    }
}

