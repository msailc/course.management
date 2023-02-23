using System.Threading.Tasks;
using PagebaTask.Entities;

namespace PagebaTask.Services
{
    public interface IAuthService
    {
        Task<string> GenerateJwtTokenAsync(User user);
        Task<User> LoginAsync(string username, string password);
        Task<User> RegisterAsync(string username, string password);
    }
}