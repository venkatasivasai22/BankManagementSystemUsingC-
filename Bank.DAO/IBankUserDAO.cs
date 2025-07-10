using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAO
{
    public interface IBankUserDAO
    {
        Task InsertBankUserDetailsAsync(BankUserDetails bankUserDetails);
        Task<List<BankUserDetails>> SelectAllBankUserDetailsAsync();
        Task<int> UpdatePinAndAccountNumberAsync(int pin, int accountNumber, int id);
        Task<int> UpdatePinNumberByUsingId(int pin, int id);
        Task<int> DeleteAsync(int id);
        Task<BankUserDetails> GetUserDetailsByUsingEmailAndPasswordAsync(string emailId, int pin);
        Task<int> UpdateAmountByUsingAccountNumberAsync(double amount, int account);
        Task<int> DebitAsync(string email, double amount);
        Task<BankUserDetails> PhoneNumberDetailsAsync(long number);
        Task<int> PhoneAmountTransferAsync(long number, double amount);
    }
}
