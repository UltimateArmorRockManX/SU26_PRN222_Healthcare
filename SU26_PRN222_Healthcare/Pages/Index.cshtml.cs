using CareDataAccess;
using CareRepositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace SU26_PRN222_Healthcare.Pages
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(ISessionRepository sessionRepo, IUserRepository userRepo) : base(sessionRepo, userRepo)
        {
        }
    }
}

