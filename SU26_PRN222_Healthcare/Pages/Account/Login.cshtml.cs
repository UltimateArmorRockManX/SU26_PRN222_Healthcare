using System;
using CareBusiness.Models;
using CareDataAccess;
using CareRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SU26_PRN222_Healthcare.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly ISessionRepository _sessionRepo;

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public string ReturnUrl { get; set; } = default!;

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = default!;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = default!;
        }

        public LoginModel(IUserRepository userRepo, ISessionRepository sessionRepo)
        {
            _userRepo = userRepo;
            _sessionRepo = sessionRepo;
        }

        public IActionResult OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userRepo.GetByEmailAndPasswordAsync(Input.Email, Input.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return Page();
            }

            var sessionId = Guid.NewGuid().ToString();
            var session = new Session
            {
                SessionID = sessionId,
                UserID = user.ID,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };

            await _sessionRepo.CreateAsync(session);

            Response.Cookies.Append("SessionId", sessionId, new CookieOptions
            {
                HttpOnly = true,
                Expires = session.ExpiresAt
            });

            if (user.Role == "Admin")
            {
                return RedirectToPage("/Admin/Doctors/Index");
            }
            return RedirectToPage("/Patient/Search");
        }
    }
}

