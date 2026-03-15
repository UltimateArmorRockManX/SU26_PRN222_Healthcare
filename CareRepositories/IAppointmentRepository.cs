using CareBusiness.Models;
using System.Threading.Tasks;

namespace CareRepositories
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetByPatientDoctorDateAsync(int patientId, int doctorId, DateTime date);
        Task<int> CountDoctorAppointmentsOnDateAsync(int doctorId, DateTime date);
        Task CreateAsync(Appointment appointment);
    }
}

