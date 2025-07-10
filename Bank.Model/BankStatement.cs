using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Model
{
    public class BankStatement
    {
        public int Transaction { get; set; }
        public double TransactionAmount { get; set; }
        public double BalanceAmount { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public TimeSpan TimeOfTransaction { get; set; }
        public string TransactionType { get; set; }
        public int UserId { get; set; }

        public BankStatement() { }

        public BankStatement(int transaction, double transactionAmount, double balanceAmount, DateTime dateOfTransaction,
                             TimeSpan timeOfTransaction, string transactionType, int userId)
        {
            Transaction = transaction;
            TransactionAmount = transactionAmount;
            BalanceAmount = balanceAmount;
            DateOfTransaction = dateOfTransaction;
            TimeOfTransaction = timeOfTransaction;
            TransactionType = transactionType;
            UserId = userId;
        }
    }
}
