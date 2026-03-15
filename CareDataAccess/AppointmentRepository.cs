using CareBusiness.Models;
using CareRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CareDataAccess
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly FptCareContext _context;

        public AppointmentRepository(FptCareContext context)
        {
            _context = context;
        }

        public async Task<Appointment> GetByPatientDoctorDateAsync(int patientId, int doctorId, DateTime date)
        {
            return await _context.Appointments
                .FirstOrDefaultAsync(a => a.PatientID == patientId &&
                                          a.DoctorID == doctorId &&
                                          a.AppointmentDate.Date == date.Date &&
                                          !a.IsCancelled);
        }

        public async Task<int> CountDoctorAppointmentsOnDateAsync(int doctorId, DateTime date)
        {
            return await _context.Appointments
                .CountAsync(a => a.DoctorID == doctorId &&
                                 a.AppointmentDate.Date == date.Date &&
                                 !a.IsCancelled);
        }

        public async Task CreateAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }
    }
}

