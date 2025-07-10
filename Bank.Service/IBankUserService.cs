using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Service
{
    // Level 3 - User specific operations
    public interface IBankUserService
        //: IAuthenticationService
    {
        Task UserRegistrationAsync();
        Task UserLoginAsync();
        Task UserOptionAsync(string emailId, int password);
        Task DebitAsync(string emailId, int password);
        Task CheckBalanceAsync(string email, int password);
        Task NumberToNumberAsync(string email, int password);
        Task CreditAsync(string emailId, int password);
        Task AutoGenerateUserRegistrationAsync();
        bool IsValidPhoneNumber(string phoneNumber);
    }
}
