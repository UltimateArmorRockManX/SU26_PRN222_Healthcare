using CareBusiness.Models;
using CareRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareDataAccess
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly FptCareContext _context;

        public DoctorRepository(FptCareContext context)
        {
            _context = context;
        }

public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.Where(d => d.Active).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return await GetAllAsync();
            }
            return await _context.Doctors
                .Where(d => d.Active &&
                            (d.DoctorName.Contains(query) ||
                             d.Specialty.Contains(query) ||
                             d.LicenseNumber.Contains(query)))
                .ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task AddAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> LicenseNumberExistsAsync(string licenseNumber)
        {
            return await _context.Doctors.AnyAsync(e => e.LicenseNumber == licenseNumber);
        }
    }
}
