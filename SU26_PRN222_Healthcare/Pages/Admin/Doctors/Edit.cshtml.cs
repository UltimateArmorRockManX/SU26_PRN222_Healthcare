using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareBusiness.Models;
using CareRepositories;

namespace SU26_PRN222_Healthcare.Pages.Admin.Doctors
{
    public class EditModel : PageModel
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly FptCareContext _context;


        public EditModel(IDoctorRepository doctorRepository, FptCareContext context)
        {
            _doctorRepository = doctorRepository;
            _context = context;

        }

        [BindProperty]
        public Doctor Doctor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor =  await _doctorRepository.GetByIdAsync(id.Value);
            if (doctor == null)
            {
                return NotFound();
            }
            Doctor = doctor;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Custom Validation
            var existingDoctor = await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.LicenseNumber == Doctor.LicenseNumber);
            if (existingDoctor != null && existingDoctor.ID != Doctor.ID)
            {
                ModelState.AddModelError("Doctor.LicenseNumber", "This license number is already in use by another doctor.");
            }
            
            if (Doctor.MaxPatients < 0)
            {
                ModelState.AddModelError("Doctor.MaxPatients", "Max Patients cannot be a negative number.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _doctorRepository.UpdateAsync(Doctor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DoctorExists(Doctor.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> DoctorExists(int id)
        {
            return await _doctorRepository.GetByIdAsync(id) != null;
        }
    }
}
