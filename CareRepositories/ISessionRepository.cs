using CareBusiness.Models;
using System.Threading.Tasks;

namespace CareRepositories
{
    public interface ISessionRepository
    {
        Task<Session> GetByIdAsync(string sessionId);
        Task CreateAsync(Session session);
        Task DeleteAsync(string sessionId);
    }
}
