using CareBusiness.Models;
using CareDataAccess;
using CareRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace SU26_PRN222_Healthcare.Pages
{
    public abstract class BasePageModel : PageModel
    {
        protected readonly ISessionRepository _sessionRepo;
        protected readonly IUserRepository _userRepo;
        protected string? CurrentSessionId => Request.Cookies["SessionId"];
        public User? CurrentUser { get; protected set; }
        public string? CurrentRole { get; protected set; }

        public BasePageModel(ISessionRepository sessionRepo, IUserRepository userRepo)
        {
            _sessionRepo = sessionRepo;
            _userRepo = userRepo;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            if (!await CheckSessionAsync())
                return RedirectToPage("/Account/Login");
            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            if (!await CheckSessionAsync())
                return RedirectToPage("/Account/Login");
            return Page();
        }

        protected async Task<bool> CheckSessionAsync()
        {
            if (string.IsNullOrEmpty(CurrentSessionId))
            {
                RedirectToPage("/Account/Login");
                return false;
            }

            var session = await _sessionRepo.GetByIdAsync(CurrentSessionId);
            if (session == null || session.ExpiresAt < DateTime.UtcNow)
            {
                Response.Cookies.Delete("SessionId");
                RedirectToPage("/Account/Login");
                return false;
            }

            CurrentUser = session.User;
            CurrentRole = session.Role;
            return true;
        }

        public bool IsAdmin => CurrentRole == "Admin";
        public bool IsPatient => CurrentRole == "Patient";
        public int CurrentUserId => CurrentUser?.ID ?? 0;
    }
}

