using Bank.DAO;
using Bank.Exception;
using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Service
{
    public class AdminServiceImp : IAdminService
    {
        private readonly IAdminDAO _adminDAO;
        private readonly IBankUserDAO _bankUserDAO;
        private List<BankUserDetails> _allBankUserDetails;
        private readonly Random _random = new Random();
        private int _count = 1;

        public AdminServiceImp()
        {
            _adminDAO = new AdminDAOImplementation();
            _bankUserDAO = new BankUserDAOImplementation();
        }

        public AdminServiceImp(IAdminDAO adminDAO, IBankUserDAO bankUserDAO)
        {
            _adminDAO = adminDAO;
            _bankUserDAO = bankUserDAO;
        }

        public async Task AdminLoginAsync()
        {
            _allBankUserDetails = await _bankUserDAO.SelectAllBankUserDetailsAsync();
            Console.WriteLine("Enter the Email: ");
            string emailId = Console.ReadLine();
            Console.WriteLine("Enter the Admin Password: ");
            string password = Console.ReadLine();

            try
            {
                if (await _adminDAO.GetAdminDetailsByEmailAndPasswordAsync(emailId, password))
                {
                    Console.WriteLine("Enter 1 To Get All UserDetails");
                    Console.WriteLine("Enter 2 To Get All Account Request Details");
                    Console.WriteLine("Enter 3 To Get All Account Closing Request Details");

                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            Console.WriteLine("All UserDetails");
                            await AllUserDetailsAsync();
                            break;
                        case 2:
                            Console.WriteLine("All Account Request Details");
                            await AcceptAllAccountRequestDetailsAsync();
                            break;
                        case 3:
                            Console.WriteLine("All Account Closing Request Details");
                            break;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;
                    }
                }
                else
                {
                    throw new AdminException("INVALID CREDENTIALS");
                }
            }
            catch (AdminException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task AcceptAllAccountRequestDetailsAsync()
        {
            var pendingData = _allBankUserDetails
                .Where(user => user.Status.Equals("pending", StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var pendingDataItem in pendingData)
            {
                Console.WriteLine($"S.No : {_count++}");
                Console.WriteLine($"User Name = {pendingDataItem.Name}");
                Console.WriteLine($"User Email ID = {pendingDataItem.EmailId}");
                Console.WriteLine($"User Status = {pendingDataItem.Status}");
                Console.WriteLine($"User Aadhar Number = {MaskAadharNumber(pendingDataItem.AadharNumber)}");
                Console.WriteLine($"User Mobile Number = {MaskMobileNumber(pendingDataItem.MobileNumber)}");
                Console.WriteLine("*****************************************************");
            }

            Console.WriteLine("Enter serial number to select User Details");
            var bankUserDetails = pendingData[int.Parse(Console.ReadLine()) - 1];

            Console.WriteLine("Enter \n 1. Accept \n 2. Reject");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    await AcceptUserRequestAsync(bankUserDetails);
                    break;
                case 2:
                    await RejectUserRequestAsync(bankUserDetails);
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }

        public async Task AcceptUserRequestAsync(BankUserDetails bankUserDetails)
        {
            int pin = GenerateRandomNumber(1000, 9999);
            int accountNumber = GenerateRandomNumber(1000000, 9999999);

            int result = await _bankUserDAO.UpdatePinAndAccountNumberAsync(pin, accountNumber, bankUserDetails.Id);
            if (result > 0)
            {
                Console.WriteLine("Account Accepted Successfully...");
                Console.WriteLine($"{bankUserDetails.Name}'s Account number is: {accountNumber}");
                Console.WriteLine($"{bankUserDetails.Name}'s Pin number is: {pin}");
            }
            else
            {
                throw new AdminException("Invalid Credentials");
            }
        }

        public async Task RejectUserRequestAsync(BankUserDetails bankUserDetails)
        {
            int result = await _bankUserDAO.DeleteAsync(bankUserDetails.Id);
            if (result != 0)
            {
                Console.WriteLine("Account rejected successfully");
            }
            else
            {
                throw new AdminException("Unable to reject");
            }
        }

        public async Task AllUserDetailsAsync()
        {
            var allDetails = await _bankUserDAO.SelectAllBankUserDetailsAsync();
            foreach (var details in allDetails)
            {
                Console.WriteLine($"User ID: {details.Id}");
                Console.WriteLine($"User Name: {details.Name}");
                Console.WriteLine($"User Email ID: {details.EmailId}");
                Console.WriteLine($"User Aadhar Number: {details.AadharNumber}");
                Console.WriteLine($"User PAN Number: {details.PanNumber}");
                Console.WriteLine($"User Mobile Number: {details.MobileNumber}");
                Console.WriteLine($"User Address: {details.Address}");
                Console.WriteLine($"User Gender: {details.Gender}");
                Console.WriteLine($"User Account Number: {details.AccountNumber}");
                Console.WriteLine($"User Status: {details.Status}");
                Console.WriteLine("============================================================");
            }
        }

        private string MaskAadharNumber(long aadharNumber)
        {
            string aadharStr = aadharNumber.ToString();
            return $"{aadharStr.Substring(0, 4)}****{aadharStr.Substring(aadharStr.Length - 3)}";
        }

        private string MaskMobileNumber(long mobileNumber)
        {
            string mobileStr = mobileNumber.ToString();
            return $"{mobileStr.Substring(0, 3)}****{mobileStr.Substring(mobileStr.Length - 3)}";
        }

        private int GenerateRandomNumber(int min, int max)
        {
            return _random.Next(min, max + 1);
        }
        // ======================================================================================
        //async Task<bool> IAuthenticationService.LoginAsync(string email, string password)
        //{
        //    bool isValid = await _adminDAO.GetAdminDetailsByEmailAndPasswordAsync(email, password);
        //    if (isValid)
        //    {
        //        LogActivity($"Admin {email} logged in successfully");
        //    }
        //    return isValid;
        //}

       
        //async Task IAuthenticationService.LogoutAsync()
        //{
        //    LogActivity("Admin logged out");
        //    await Task.CompletedTask;
        //}

      
        //async Task<bool> IBaseService.ValidateAsync()
        //{
        //    LogActivity("Admin validation started");
        //    return await Task.FromResult(true);
        //}

       
        //public void LogActivity(string message)
        //{
        //    Console.WriteLine($"[ADMIN LOG] {DateTime.Now}: {message}");
        //}

    }
}
