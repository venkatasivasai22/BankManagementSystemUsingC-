using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Model
{
    public class BankUserDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public long AadharNumber { get; set; }
        public string PanNumber { get; set; }
        public long MobileNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public double Amount { get; set; }
        public int Pin { get; set; }
        public int AccountNumber { get; set; }
        public string Status { get; set; }

        public BankUserDetails() { }

        public BankUserDetails(int id, string name, string emailId, long aadharNumber, string panNumber,
                               long mobileNumber, string address, string gender, double amount, int pin,
                               int accountNumber, string status)
        {
            Id = id;
            Name = name;
            EmailId = emailId;
            AadharNumber = aadharNumber;
            PanNumber = panNumber;
            MobileNumber = mobileNumber;
            Address = address;
            Gender = gender;
            Amount = amount;
            Pin = pin;
            AccountNumber = accountNumber;
            Status = status;
        }

        public override string ToString()
        {
            return $"BankUserDetails [Id={Id}, Name={Name}, EmailId={EmailId}, AadharNumber={AadharNumber}, " +
                   $"PanNumber={PanNumber}, MobileNumber={MobileNumber}, Address={Address}, Gender={Gender}, " +
                   $"Amount={Amount}, Pin={Pin}, AccountNumber={AccountNumber}, Status={Status}]";
        }
    }
}
