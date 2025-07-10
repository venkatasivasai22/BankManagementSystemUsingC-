using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exception
{
    public class BankUserException : FormatException
    {
        public string Msg { get; set; }

        public BankUserException(string msg) : base(msg)
        {
            Msg = msg;
        }
    }
}
