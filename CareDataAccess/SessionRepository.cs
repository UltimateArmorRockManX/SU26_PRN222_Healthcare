using CareBusiness.Models;
using CareRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CareDataAccess
{
    public class SessionRepository : ISessionRepository
    {
        private readonly FptCareContext _context;

        public SessionRepository(FptCareContext context)
        {
            _context = context;
        }

        public async Task<Session> GetByIdAsync(string sessionId)
        {
            return await _context.Sessions
                .Include(s => s.User) // Also load the related User data
                .FirstOrDefaultAsync(s => s.SessionID == sessionId);
        }

        public async Task CreateAsync(Session session)
        {
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string sessionId)
        {
            var session = await _context.Sessions.FindAsync(sessionId);
            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }
    }
}
