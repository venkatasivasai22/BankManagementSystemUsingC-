using System.Threading.Tasks;

namespace Bank.Service
{
    // Level 2 - Authentication operations
    public interface IAuthenticationService : IBaseService
    {
        Task<bool> LoginAsync(string email, string password);
        Task LogoutAsync();
    }
}