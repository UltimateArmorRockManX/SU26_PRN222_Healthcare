using CareBusiness.Models;
using CareDataAccess;
using CareRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SU26_PRN222_Healthcare.Pages.Patient
{
    public class BookModel : BasePageModel
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IAppointmentRepository _appointmentRepo;

        [BindProperty]
        public InputModel Input { get; set; } = default!;
        public SelectList? DoctorList { get; set; }
        public List<Doctor> Doctors { get; set; } = new();

        public class InputModel
        {
            [Required]
            public int DoctorId { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime AppointmentDate { get; set; }
        }

        public BookModel(
            ISessionRepository sessionRepo,
            IUserRepository userRepo,
            IDoctorRepository doctorRepo,
            IAppointmentRepository appointmentRepo) : base(sessionRepo, userRepo)
        {
            _doctorRepo = doctorRepo;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!await CheckSessionAsync() || !IsPatient)
            {
                return RedirectToPage("/Account/Login");
            }

            Doctors = (await _doctorRepo.GetAllAsync()).ToList();
            DoctorList = new SelectList(Doctors, "ID", "DoctorName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!await CheckSessionAsync() || !IsPatient)
            {
                return RedirectToPage("/Account/Login");
            }

            if (!ModelState.IsValid)
            {
                Doctors = (await _doctorRepo.GetAllAsync()).ToList();
                DoctorList = new SelectList(Doctors, "ID", "DoctorName");
                return Page();
            }

            var doctor = await _doctorRepo.GetByIdAsync(Input.DoctorId);
            if (doctor == null || !doctor.Active)
            {
                ModelState.AddModelError("Input.DoctorId", "Invalid or inactive doctor.");
                Doctors = (await _doctorRepo.GetAllAsync()).ToList();
                DoctorList = new SelectList(Doctors, "ID", "DoctorName");
                return Page();
            }

            var existingAppt = await _appointmentRepo.GetByPatientDoctorDateAsync(CurrentUserId, Input.DoctorId, Input.AppointmentDate.Date);
            if (existingAppt != null)
            {
                ModelState.AddModelError("", "You have already booked this doctor for the same day.");
                return Page();
            }

            var count = await _appointmentRepo.CountDoctorAppointmentsOnDateAsync(Input.DoctorId, Input.AppointmentDate.Date);
            if (count >= doctor.MaxPatients)
            {
                ModelState.AddModelError("", $"Doctor has reached max patients ({doctor.MaxPatients}) for {Input.AppointmentDate:yyyy-MM-dd}.");
                return Page();
            }

            var appointment = new Appointment
            {
                PatientID = CurrentUserId,
                DoctorID = Input.DoctorId,
                AppointmentDate = Input.AppointmentDate.Date,
                BookingDate = DateTime.Now,
                IsCancelled = false
            };

            await _appointmentRepo.CreateAsync(appointment);

            TempData["success"] = "Appointment booked successfully!";
            return RedirectToPage("/Patient/Search");
        }
    }
}

