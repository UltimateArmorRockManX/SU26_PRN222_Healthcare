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
    public class IndexModel : PageModel
    {
        private readonly IDoctorRepository _doctorRepository;

        public IndexModel(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public IList<Doctor> Doctor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Doctor = (await _doctorRepository.GetAllAsync()).ToList();
        }
    }
}
