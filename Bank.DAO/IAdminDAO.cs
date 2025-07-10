using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAO
{
    public interface IAdminDAO
    {
        Task<bool> GetAdminDetailsByEmailAndPasswordAsync(string email, string password);
    }
}
