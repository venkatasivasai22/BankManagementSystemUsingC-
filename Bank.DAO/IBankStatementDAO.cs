using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAO
{
    public interface IBankStatementDAO
    {
        Task<int> InsertStatementDetailsAsync(BankStatement bankStatement);
    }
}
