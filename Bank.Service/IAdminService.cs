using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Service
{
    // Level 3 - Admin specific operations
    public interface IAdminService 
        //: IAuthenticationService
    {
        Task AdminLoginAsync();
        Task AcceptAllAccountRequestDetailsAsync();
        Task AcceptUserRequestAsync(BankUserDetails bankUserDetails);
        Task RejectUserRequestAsync(BankUserDetails bankUserDetails);
        Task AllUserDetailsAsync();
    }
}
