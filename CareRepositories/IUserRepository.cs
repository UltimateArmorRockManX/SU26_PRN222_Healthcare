using CareBusiness.Models;
using System.Threading.Tasks;

namespace CareRepositories
{
public interface IUserRepository
    {
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
        Task<User> GetByIdAsync(int id);
    }
}
