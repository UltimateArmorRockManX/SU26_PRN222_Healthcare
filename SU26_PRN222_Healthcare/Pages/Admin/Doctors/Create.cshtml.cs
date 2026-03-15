using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CareBusiness.Models;
using CareRepositories;

namespace SU26_PRN222_Healthcare.Pages.Admin.Doctors
{
    public class CreateModel : PageModel
    {
        private readonly IDoctorRepository _doctorRepository;

        public CreateModel(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Doctor Doctor { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Custom Validation
            if (await _doctorRepository.LicenseNumberExistsAsync(Doctor.LicenseNumber))
            {
                ModelState.AddModelError("Doctor.LicenseNumber", "This license number already exists.");
            }

            if (Doctor.MaxPatients < 0)
            {
                ModelState.AddModelError("Doctor.MaxPatients", "Max Patients cannot be a negative number.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _doctorRepository.AddAsync(Doctor);

            return RedirectToPage("./Index");
        }
    }
}
