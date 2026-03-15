using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CareBusiness.Models;
using CareRepositories;

namespace SU26_PRN222_Healthcare.Pages.Admin.Doctors
{
    public class DetailsModel : PageModel
    {
        private readonly IDoctorRepository _doctorRepository;

        public DetailsModel(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

      public Doctor Doctor { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _doctorRepository.GetByIdAsync(id.Value);
            if (doctor == null)
            {
                return NotFound();
            }
            else 
            {
                Doctor = doctor;
            }
            return Page();
        }
    }
}
