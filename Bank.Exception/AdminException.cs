using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exception
{
    public class AdminException : FormatException
    {
        public string Msg { get; set; }

        public AdminException(string msg) : base(msg)
        {
            Msg = msg;
        }
    }
}
