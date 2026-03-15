using CareBusiness.Models;
using CareRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CareDataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly FptCareContext _context;

        public UserRepository(FptCareContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}

