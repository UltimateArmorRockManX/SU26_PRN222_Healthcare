using CareBusiness.Models;
using CareDataAccess;
using CareRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SU26_PRN222_Healthcare.Pages.Patient
{
    public class SearchModel : BasePageModel
    {
        private readonly IDoctorRepository _doctorRepo;

        public string? SearchTerm { get; set; }
        public List<Doctor> Doctors { get; set; } = new();

        public SearchModel(ISessionRepository sessionRepo, IUserRepository userRepo, IDoctorRepository doctorRepo) : base(sessionRepo, userRepo)
        {
            _doctorRepo = doctorRepo;
        }

        public override async Task<IActionResult> OnGetAsync()
        {
            if (!await CheckSessionAsync() || !IsPatient)
            {
                return RedirectToPage("/Account/Login");
            }

            SearchTerm = "";
            Doctors = (await _doctorRepo.SearchAsync("")).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string searchTerm)
        {
            if (!await CheckSessionAsync() || !IsPatient)
            {
                return RedirectToPage("/Account/Login");
            }

            SearchTerm = searchTerm;
            Doctors = (await _doctorRepo.SearchAsync(SearchTerm ?? "")).ToList();
            return Page();
        }
    }
}
