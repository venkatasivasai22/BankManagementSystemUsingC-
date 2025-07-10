using Bank.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAO
{
    public class BankStatementDAOImplementation : IBankStatementDAO
    {
        private static readonly string connectionString = "Server=INHYNVMUSAM01;Database=BankManagement;Trusted_Connection=True;";
        private static readonly string insertQuery = "INSERT INTO bank_statements (Transaction_Amount, Balance_Amount, Date_Of_Transaction, Time_Of_Transaction, Transaction_Type, User_Id) VALUES (@TransactionAmount, @BalanceAmount, @DateOfTransaction, @TimeOfTransaction, @TransactionType, @UserId);";

        public async Task<int> InsertStatementDetailsAsync(BankStatement bankStatement)
        {
            try
            {
                Console.WriteLine($"Attempting to insert statement for UserId: {bankStatement.UserId}");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionAmount", bankStatement.TransactionAmount);
                        command.Parameters.AddWithValue("@BalanceAmount", bankStatement.BalanceAmount);
                        command.Parameters.AddWithValue("@DateOfTransaction", bankStatement.DateOfTransaction);
                        command.Parameters.AddWithValue("@TimeOfTransaction", bankStatement.TimeOfTransaction);
                        command.Parameters.AddWithValue("@TransactionType", bankStatement.TransactionType);
                        command.Parameters.AddWithValue("@UserId", bankStatement.UserId);
                        return await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                return 0;
            }
        }
    }
}
