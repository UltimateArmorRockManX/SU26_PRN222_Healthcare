using CareBusiness.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareRepositories
{
public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<IEnumerable<Doctor>> SearchAsync(string query);
        Task<Doctor> GetByIdAsync(int id);
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(int id);
        Task<bool> LicenseNumberExistsAsync(string licenseNumber);
    }
}
