using Bank.DAO;
using Bank.Exception;
using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bank.Service
{
    public class BankUserServiceImp : IBankUserService
    {
        private readonly IBankUserDAO _bankUserDAO;
        private readonly IBankStatementDAO _bankStatementDAO;

        public BankUserServiceImp()
        {
            _bankUserDAO = new BankUserDAOImplementation();
            _bankStatementDAO = new BankStatementDAOImplementation();
        }

        public BankUserServiceImp(IBankUserDAO bankUserDAO, IBankStatementDAO bankStatementDAO)
        {
            _bankUserDAO = bankUserDAO;
            _bankStatementDAO = bankStatementDAO;
        }

        public async Task AutoGenerateUserRegistrationAsync()
        {
            var allBankUserDetails = await _bankUserDAO.SelectAllBankUserDetailsAsync();
            var bankUserDetails = new BankUserDetails();
            var random = new Random();

            // Auto-generate Name
            string[] firstNames = { "John", "Jane", "Mike", "Sarah", "David", "Lisa", "Tom", "Anna" };
            string[] lastNames = { "Smith", "Johnson", "Brown", "Davis", "Wilson", "Miller", "Moore", "Taylor" };
            bankUserDetails.Name = $"{firstNames[random.Next(firstNames.Length)]} {lastNames[random.Next(lastNames.Length)]}";

            // Auto-generate unique Email
            string baseEmail;
            do
            {
                baseEmail = $"user{random.Next(1000, 9999)}@gmail.com";
            } while (allBankUserDetails.Any(user => user.EmailId == baseEmail));
            bankUserDetails.EmailId = baseEmail;

            // Auto-generate unique Mobile Number (10 digits starting with 6-9)
            long mobileNumber;
            do
            {
                mobileNumber = long.Parse($"{random.Next(6, 10)}{random.Next(100000000, 999999999)}");
            } while (allBankUserDetails.Any(user => user.MobileNumber == mobileNumber));
            bankUserDetails.MobileNumber = mobileNumber;

            // Auto-generate unique PAN Number (5 letters + 4 digits + 1 letter)
            string panNumber;
            do
            {
                string letters1 = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 5)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                string digits = random.Next(1000, 9999).ToString();
                string letter2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[random.Next(26)].ToString();
                panNumber = letters1 + digits + letter2;
            } while (allBankUserDetails.Any(user => user.PanNumber == panNumber));
            bankUserDetails.PanNumber = panNumber;

            // Auto-generate unique Aadhar Number (12 digits)
            long aadharNumber;
            do
            {
                aadharNumber = long.Parse($"{random.Next(100000, 999999)}{random.Next(100000, 999999)}");
            } while (allBankUserDetails.Any(user => user.AadharNumber == aadharNumber));
            bankUserDetails.AadharNumber = aadharNumber;

            // Auto-generate Address
            string[] addresses = { "123 Main St", "456 Oak Ave", "789 Pine Rd", "321 Elm St", "654 Maple Dr" };
            bankUserDetails.Address = addresses[random.Next(addresses.Length)];

            // Auto-generate Gender
            string[] genders = { "Male", "Female", "Others" };
            bankUserDetails.Gender = genders[random.Next(genders.Length)];

            // Auto-generate Initial Amount
            bankUserDetails.Amount = random.Next(1000, 50000);

            await _bankUserDAO.InsertBankUserDetailsAsync(bankUserDetails);
            Console.WriteLine($"Auto-generated user: {bankUserDetails.Name} with email: {bankUserDetails.EmailId}");
        }


        public bool IsValidPhoneNumber(string phoneNumber)
        {

            string pattern = @"^[6-9]\d{9}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public async Task UserRegistrationAsync()
        {

            var allBankUserDetails = await _bankUserDAO.SelectAllBankUserDetailsAsync();
            var bankUserDetails = new BankUserDetails();

            // Name
            Console.WriteLine("Enter Your Name: ");
            bankUserDetails.Name = Console.ReadLine();

            // Email
            bool emailStatus = true;
            while (emailStatus)
            {
                try
                {
                    Console.WriteLine("Enter Your Email: ");
                    string email = Console.ReadLine();
                    if (email.EndsWith("@gmail.com"))
                    {
                        if (!allBankUserDetails.Any(user => user.EmailId == email))
                        {
                            bankUserDetails.EmailId = email;
                            emailStatus = false;
                        }
                        else
                        {
                            throw new BankUserException("Email ID already exists");
                        }
                    }
                    else
                    {
                        throw new BankUserException("Invalid Email");
                    }
                }
                catch (BankUserException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Mobile Number
            bool mobileStatus = true;
            while (mobileStatus)
            {
                try
                {
                    Console.WriteLine("Enter Your Mobile Number: ");
                    long mobileNumber = long.Parse(Console.ReadLine());
                    if (IsValidPhoneNumber(mobileNumber.ToString()))
                    {
                        if (!allBankUserDetails.Any(user => user.MobileNumber == mobileNumber))
                        {
                            bankUserDetails.MobileNumber = mobileNumber;
                            mobileStatus = false;
                        }
                        else
                        {
                            throw new BankUserException("Mobile Number already exists");
                        }
                    }
                    else
                    {
                        throw new BankUserException("Invalid Mobile Number");
                    }
                }
                catch (BankUserException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input. Please enter a valid number.");
                }
            }

            // PAN Number
            bool panStatus = true;
            while (panStatus)
            {
                try
                {
                    Console.WriteLine("Enter Your PAN Number: ");
                    string panNumber = Console.ReadLine();
                    if (panNumber.Length == 10 &&
                        panNumber.Take(5).All(char.IsUpper) &&
                        panNumber.Skip(5).Take(4).All(char.IsDigit) &&
                        char.IsUpper(panNumber[9]))
                    {
                        if (!allBankUserDetails.Any(user => user.PanNumber == panNumber))
                        {
                            bankUserDetails.PanNumber = panNumber;
                            panStatus = false;
                        }
                        else
                        {
                            throw new BankUserException("PAN Number already exists");
                        }
                    }
                    else
                    {
                        throw new BankUserException("Invalid PAN Number");
                    }
                }
                catch (BankUserException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Aadhar Number
            bool aadharStatus = true;
            while (aadharStatus)
            {
                try
                {
                    Console.WriteLine("Enter Your Aadhar Number: ");
                    long aadharNumber = long.Parse(Console.ReadLine());
                    if (aadharNumber >= 100000000000 && aadharNumber <= 999999999999)
                    {
                        if (!allBankUserDetails.Any(user => user.AadharNumber == aadharNumber))
                        {
                            bankUserDetails.AadharNumber = aadharNumber;
                            aadharStatus = false;
                        }
                        else
                        {
                            throw new BankUserException("Aadhar Number already exists");
                        }
                    }
                    else
                    {
                        throw new BankUserException("Invalid Aadhar Number");
                    }
                }
                catch (BankUserException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input. Please enter a valid number.");
                }
            }

            // Address
            Console.WriteLine("Enter Your Address: ");
            bankUserDetails.Address = Console.ReadLine();

            // Gender
            bool genderStatus = true;
            while (genderStatus)
            {
                try
                {
                    Console.WriteLine("Enter Your Gender (Male/Female/Others): ");
                    string gender = Console.ReadLine();
                    if (gender.Equals("Male", StringComparison.OrdinalIgnoreCase) || gender.Equals("Female", StringComparison.OrdinalIgnoreCase) ||
                        gender.Equals("Others", StringComparison.OrdinalIgnoreCase))
                    {
                        bankUserDetails.Gender = gender;
                        genderStatus = false;
                    }
                    else
                    {
                        throw new BankUserException("Invalid Gender");
                    }
                }
                catch (BankUserException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Amount
            Console.WriteLine("Enter Your Initial Amount: ");
            bankUserDetails.Amount = double.Parse(Console.ReadLine());

            await _bankUserDAO.InsertBankUserDetailsAsync(bankUserDetails);
            Console.WriteLine("User registered successfully!");
        }

        public async Task UserLoginAsync()
        {
            Console.WriteLine("Enter Email ID: ");
            string emailId = Console.ReadLine();
            Console.WriteLine("Enter PIN: ");
            int pin = int.Parse(Console.ReadLine());

            var userDetails = await _bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(emailId, pin);

            if (userDetails != null)
            {
                Console.WriteLine($"Hello {(userDetails.Gender == "Male" ? "Mr." : "Miss")} {userDetails.Name}");
                await UserOptionAsync(emailId, pin);
            }
            else
            {
                Console.WriteLine("Invalid Email or PIN.");
            }
        }

        public async Task UserOptionAsync(string emailId, int password)
        {
            Console.WriteLine("Enter \n 1. To Debit \n 2. To Credit \n 3. To Check Balance \n 4. To Transfer Money \n 5. To Cancle Account requst");
            switch (Console.ReadLine())
            {
                case "1":
                    await DebitAsync(emailId, password);
                    break;
                case "2":
                    await CreditAsync(emailId, password);
                    break;
                case "3":
                    await CheckBalanceAsync(emailId, password);
                    break;
                case "4":
                    await NumberToNumberAsync(emailId, password);
                    break;
                case "5":

                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
           
        }

        public async Task DebitAsync(string emailId, int password)
        {
            BankUserDetails userDetails = await _bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(emailId, password);
            if (userDetails == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            Console.WriteLine("Enter Amount to Debit: ");
            double userammount = Convert.ToDouble(Console.ReadLine());
            double databasebalance = userDetails.Amount;
            double balanceamount = databasebalance - userammount;
            Console.WriteLine("Total Balance: " + databasebalance);

            try
            {
                if (userammount >= 0)
                {
                    if (userammount <= databasebalance)
                    {
                        int result = await _bankUserDAO.DebitAsync(emailId, balanceamount);
                        if (result > 0)
                        {
                            var bankStatement = new BankStatement
                            {
                                BalanceAmount = balanceamount,
                                TransactionAmount = userammount,
                                DateOfTransaction = DateTime.Now.Date,
                                TimeOfTransaction = DateTime.Now.TimeOfDay,
                                TransactionType = "Debit",
                                UserId = userDetails.Id
                            };
                            await _bankStatementDAO.InsertStatementDetailsAsync(bankStatement);

                            Console.WriteLine("Amount debited successfully");
                        }
                        else
                        {
                            throw new BankUserException("Server error occurred.");
                        }
                    }
                    else
                    {
                        throw new BankUserException("Insufficient amount.");
                    }
                }
                else
                {
                    throw new BankUserException("Invalid amount.");
                }
            }
            catch (BankUserException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task CheckBalanceAsync(string email, int password)
        {
            var bankUserDetails = await _bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(email, password);
            if (bankUserDetails == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine($"Account holder name is: {bankUserDetails.Name}");
            Console.WriteLine($"Account balance is: {bankUserDetails.Amount}");

        }

        public async Task NumberToNumberAsync(string email, int password)
        {
            var bankUserDetails = await _bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(email, password);
            if (bankUserDetails == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("Enter the phone number where you want to transfer: ");
            long number = Convert.ToInt64(Console.ReadLine());
            var targetUser = await _bankUserDAO.PhoneNumberDetailsAsync(number);

            if (targetUser == null)
            {
                Console.WriteLine("Phone number not found.");
                return;
            }

            Console.WriteLine("Enter the amount to transfer: ");
            double amount = Convert.ToDouble(Console.ReadLine());
            double databaseAmount = bankUserDetails.Amount;

            if (databaseAmount > 0)
            {
                double balanceAmount = databaseAmount - amount;
                double targetBalance = targetUser.Amount + amount;

                if (amount <= databaseAmount)
                {
                    int debitResult = await _bankUserDAO.DebitAsync(email, balanceAmount);
                    int creditResult = await _bankUserDAO.PhoneAmountTransferAsync(number, targetBalance);

                    if (debitResult > 0 && creditResult > 0)
                    {
                        var bankStatement = new BankStatement
                        {
                            BalanceAmount = balanceAmount,
                            TransactionAmount = amount,
                            DateOfTransaction = DateTime.Now.Date,
                            TimeOfTransaction = DateTime.Now.TimeOfDay,
                            TransactionType = "Transfer",
                            UserId = bankUserDetails.Id
                        };
                        await _bankStatementDAO.InsertStatementDetailsAsync(bankStatement);

                        Console.WriteLine("Transfer successful.");
                    }
                    else
                    {
                        Console.WriteLine("Transfer failed.");
                    }
                }
                else
                {
                    Console.WriteLine("Insufficient amount.");
                }
            }
            else
            {
                Console.WriteLine("Balance is zero.");
            }

        }

        public async Task CreditAsync(string emailId, int password)
        {
            var bankUserDetails = await _bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(emailId, password);
            if (bankUserDetails == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("Enter your Amount: ");
            double userAmount = Convert.ToDouble(Console.ReadLine());
            double databaseAmount = bankUserDetails.Amount;
            double newBalance = databaseAmount + userAmount;

            Console.WriteLine($"Total amount: {databaseAmount}");

            try
            {
                if (userAmount > 0)
                {
                    int result = await _bankUserDAO.DebitAsync(emailId, newBalance);
                    if (result > 0)
                    {
                        var bankStatement = new BankStatement
                        {
                            BalanceAmount = newBalance,
                            TransactionAmount = userAmount,
                            DateOfTransaction = DateTime.Now.Date,
                            TimeOfTransaction = DateTime.Now.TimeOfDay,
                            TransactionType = "Credit",
                            UserId = bankUserDetails.Id
                        };
                        await _bankStatementDAO.InsertStatementDetailsAsync(bankStatement);

                        Console.WriteLine("Amount credited successfully.");
                    }
                    else
                    {
                        throw new BankUserException("Server error occurred.");
                    }
                }
                else
                {
                    throw new BankUserException("Invalid amount.");
                }
            }
            catch (BankUserException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // ========================================================================

        //async Task<bool> IAuthenticationService.LoginAsync(string email, string password)
        //{
        //    var user = await _bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(email, int.Parse(password));
        //    bool isValid = user != null;
        //    if (isValid)
        //    {
        //        LogActivity($"User {email} logged in successfully");
        //    }
        //    return isValid;
        //}
        // ========================================================================
        //async Task IAuthenticationService.LogoutAsync()
        //{
        //    LogActivity("User logged out");
        //    await Task.CompletedTask;
        //}

        //async Task<bool> IBaseService.ValidateAsync()
        //{
        //    LogActivity("User validation started");
        //    return await Task.FromResult(true);
        //}

        //public void LogActivity(string message)
        //{
        //    Console.WriteLine($"[USER LOG] {DateTime.Now}: {message}");
        //}
    }
}
