using System.Threading.Tasks;

namespace Bank.Service
{
    // Base interface - Level 1
    public interface IBaseService
    {
        Task<bool> ValidateAsync();
        void LogActivity(string message);
    }
}