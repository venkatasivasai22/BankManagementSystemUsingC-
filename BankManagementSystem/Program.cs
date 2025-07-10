using Bank.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IBankUserService bankUserService = new BankUserServiceImp();
            IAdminService adminService = new AdminServiceImp();
            Console.WriteLine("Welcome To Bank Management System");

            bool cont = true;

            while (cont)
            {
                Console.WriteLine("Enter 1 for User Registration");
                Console.WriteLine("Enter 2 for Admin Login");
                Console.WriteLine("Enter 3 for User Login");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("User Registration");
                            await bankUserService.AutoGenerateUserRegistrationAsync();
                            break;

                        case 2:
                            Console.WriteLine("Admin Login");
                            await adminService.AdminLoginAsync();
                            break;

                        case 3:
                            Console.WriteLine("User Login");
                            await bankUserService.UserLoginAsync();
                            break;

                        default:
                            Console.WriteLine("Invalid choice!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid number.");
                }

                Console.WriteLine("Do you want to continue? (Enter yes or no):");
                string response = Console.ReadLine();
                if (!response.Equals("yes", StringComparison.OrdinalIgnoreCase))
                {
                    cont = false;
                    Console.WriteLine("Thank you, visit again!");
                }
            }
        }
    }
}
